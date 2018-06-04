using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LogGenerator
{
    /**
     * Source Log Line
     */ 
    public class LogLine
    {
        public string iPAddress { get; private set; }
        public string accessDateStr { get; private set; }
        public string accessTimeStr { get; private set; }
        public string utcOffsetStr { get; private set; }
        public DateTime accessTime { get; private set; }
        public string method { get; private set; }
        public string url { get; private set; }
        public string type { get; private set; }
        public int statusCode { get; private set; }
        public int bytesSent { get; private set; }
        public string referer { get; private set; }
        public int duration { get; private set; }

        private const string _q = "\"";

        private static string _regextPattern = 
            @"^(\d+\.\d+\.\d+\.\d+)\s+-\s+-\s+\[(\d{2}/\w{3}/\d{4}):(\d{2}:\d{2}:\d{2})\s+([+-]\d+)\]\s+" + _q + 
            @"(\w+)\s+/(\S+)\s(\S+)" + _q + @"\s(\d+)\s(\d+)\s" + "\"-\"\\s\"" + @"(\S+)" + _q + @"\s(\d+)";
        private static Regex _regex = new Regex(_regextPattern, RegexOptions.Singleline | RegexOptions.Compiled);
        private static string dateParsePattern = @"(\d{2}/\w{3}/\d{4}:\d{2}:\d{2}:\d{2}\s+-\d+)";

        public bool ParseLine(string line)
        {
            iPAddress = null;
            accessTimeStr = null;
            url = null;
            statusCode = 0;
            bytesSent = 0;
            referer = null;
            duration = 0;
            var matches = _regex.Match(line);
            if (matches.Success)
            {
                string ws;
                int wi;

                iPAddress = matches.Groups[1].Value;
                accessDateStr = matches.Groups[2].Value;
                accessTimeStr = matches.Groups[3].Value;
                utcOffsetStr = matches.Groups[4].Value;
                method = matches.Groups[5].Value;
                url = matches.Groups[6].Value;
                type = matches.Groups[7].Value;
                ws = matches.Groups[8].Value;
                if (int.TryParse(ws, out wi))
                {
                    statusCode = wi;
                }
                ws = matches.Groups[9].Value;
                if (int.TryParse(ws, out wi))
                {
                    bytesSent = wi;
                }
                referer = matches.Groups[10].Value;

                ws = matches.Groups[11].Value;
                if (int.TryParse(ws, out wi))
                {
                    duration = wi;
                }

                
                accessTime = DateTime.Parse(accessDateStr + ' ' + accessTimeStr + utcOffsetStr).ToUniversalTime();

                return true;
            }
            return false;
        }
    }
}
