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
            string streamUrl = "stream";

            if (args.Length < 3)
            {
                Console.WriteLine("Usage:  log_generator source destination stream-id [source-url]");
                Console.WriteLine("  source: Directory or file to process. Directories should end with a '\\', or the source will be treated as a file.");
                Console.WriteLine("  destination: Destination file name.");
                Console.WriteLine("  stream-id: No spaces, usually call letters eg. KTTT, WTTTHD1, etc.");
                Console.WriteLine("  source-url: The source url for your stream, used to determine if the line represents someone accessing your stream. Defaults to /stream.xspf.");
                return;
            }

            if (args.Length > 3)
            {
                streamUrl = args[3];
            }

            var parser = new FileParser(args[0], args[1], args[2], streamUrl);
            if (!parser.isValid)
            {
                Console.WriteLine("Unable to find or access {0]", args[0]);
            }
            else
            {
                Console.WriteLine("Processing {0}.", args[0]);
                var lines = parser.parse();
                Console.WriteLine("{0} lines scanned in {1} files, {2} lines found.", parser.linesProcessed, parser.filesProcessed, lines);
            }
            Console.WriteLine("Press any key....");
            var c = Console.ReadKey();
        }


    }
}
