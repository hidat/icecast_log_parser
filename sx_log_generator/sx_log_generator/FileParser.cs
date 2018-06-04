using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogGenerator
{
    public class FileParser
    {
        string _source;
        string _destinationFile;
        string _streamId;
        string _streamUrl;
        int _minDuration;
        public long linesProcessed { get; private set; }
        public long linesFound { get; private set; }
        public int filesProcessed { get; private set; }
        public bool isDirectory { get; private set; }
        public bool isValid { get; private set; }

        public FileParser(string source, string dest, string streamId, string streamUrl, int minDuration = 10)
        {
            _source = source;
            _destinationFile = dest;
            _streamId = streamId;
            _streamUrl = streamUrl;
            _minDuration = minDuration;
            linesProcessed = 0;
            linesFound = 0;
            filesProcessed = 0;
            isValid = false;

            if (Directory.Exists(_source))
            {
                isDirectory = true;
                isValid = true;
            }
            else if (File.Exists(_source))
            {
                isValid = true;
            }
        }

        public long parse()
        {
            long foundLines = 0;
            if (isDirectory)
            {
                var sw = new StreamWriter(_destinationFile, false);
                foundLines = parseDirectory(_source, sw);
                sw.Close();
            }
            else if (isValid)
            {
                var sw = new StreamWriter(_destinationFile, false);
                foundLines = parseFile(_source, sw);
                sw.Close();
            }
            return foundLines;
        }

        protected long parseFile(string sourceFile, TextWriter stream)
        {
            long foundLines = 0;
            var sw = new StreamReader(sourceFile);
            var logLine = new LogLine();
            this.filesProcessed++;
            while (!sw.EndOfStream)
            {
                var line = sw.ReadLine();
                if (line.Length > 0)
                {
                    this.linesProcessed++;
                    if (logLine.ParseLine(line))
                    {
                        if (logLine.statusCode == 200 && logLine.url.StartsWith(_streamUrl) && logLine.duration > _minDuration)
                        {
                            foundLines++;
                            this.linesFound++;
                            stream.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", 
                                logLine.iPAddress, 
                                logLine.accessTime.ToString("yyyy-MM-dd"),
                                logLine.accessTime.ToString("HH:mm:ss"),
                                _streamId, logLine.duration, logLine.statusCode, logLine.referer);
                        }
                    }
                }
            }
            return foundLines;
        }

        protected long parseDirectory(string sourceDir, TextWriter stream, string fileMask = "*.log*")
        {
            long foundLines = 0;
            var files = Directory.EnumerateFiles(sourceDir, fileMask)
                     .OrderByDescending(filename => filename).ToList();

            foreach (var filename in files)
            {
                foundLines += parseFile(filename, stream);
            }
            return foundLines;
        }
    }
}
