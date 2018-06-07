using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 1 && (args[0] == "-h" || args[0].ToLower() == "help"))
            {
                Console.WriteLine("Usage:  log_generator source destination stream-id [source-url]");
                Console.WriteLine("  source: Directory or file to process. Directories should end with a '\\', or the source will be treated as a file.");
                Console.WriteLine("  destination: Destination file name.");
                Console.WriteLine("  stream-id: No spaces, usually call letters eg. KTTT, WTTTHD1, etc.");
                Console.WriteLine("  source-url: The source url for your stream, used to determine if the line represents someone accessing your stream. Defaults to /stream.xspf.");
                return;
            }

            string sourceDirectory = null;
            string destinationFile = null;
            string channelId = null;
            string streamUrl = null;

            if (args.Length > 0)
            {
                sourceDirectory = args[0];
            }

            if (args.Length > 1)
            {
                destinationFile = args[1];
            }

            if (args.Length > 2)
            {
                channelId = args[2];
            }

            if (args.Length > 3)
            {
                streamUrl = args[3];
            }

            Console.WriteLine("Icecast Log Parser\n");
            if (String.IsNullOrEmpty(sourceDirectory))
            {
                string val = null;
                Console.Write("Source Directory: ");
                val = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(val))
                {
                    return;
                }
                sourceDirectory = val;
            }

            if (String.IsNullOrEmpty(destinationFile))
            {
                string val = null;
                Console.Write("Destination File: ");
                val = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(val))
                {
                    return;
                }
                destinationFile = val;
            }

            if (String.IsNullOrEmpty(channelId))
            {
                string val = null;
                Console.Write("Channel ID: ");
                val = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(val))
                {
                    return;
                }
                channelId = val;
            }

            if (String.IsNullOrEmpty(streamUrl))
            {
                string val = null;
                Console.Write("Stream Url: ");
                val = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(val))
                {
                    return;
                }
                streamUrl = val;
            }

            Console.WriteLine("\nGenerating logs for:");
            Console.WriteLine("  Source Directory: {0}", sourceDirectory);
            Console.WriteLine("  Destination File: {0}", destinationFile);
            Console.WriteLine("  Channel ID: {0}", channelId);
            Console.WriteLine("  Stream URL: {0}", streamUrl);

            var parser = new FileParser(sourceDirectory, destinationFile, channelId, streamUrl);
            if (!parser.isValid)
            {
                Console.WriteLine("Unable to find or access {0]", sourceDirectory);
            }
            else
            {
                var lines = parser.parse();
                Console.WriteLine("{0} lines scanned in {1} files, {2} lines found.", parser.linesProcessed, parser.filesProcessed, lines);
            }
            Console.WriteLine("Press any key....");
            var c = Console.ReadKey();
        }


    }
}
