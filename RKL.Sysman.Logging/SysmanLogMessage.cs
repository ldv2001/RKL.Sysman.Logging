using System;
using System.Collections.Generic;
using System.Linq;

namespace RKL.Sysman.Logging
{
    public class SysmanLogMessage
    {
        internal const string dateFormat = "yyyy-MM-dd HH:mm:ss.fff";

        private List<SysmanLogMessageEntry> sysmanLogMessageEntries = new List<SysmanLogMessageEntry>();

        public long? ActionId { get; private set; } = null;
        public string Source { get; private set; }
        public string Method { get; private set; }
        public string MethodVersion { get; private set; }
        public string Status
        {
            get
            {
                if (sysmanLogMessageEntries.Count == 0)
                {
                    return SysmanLogResult.None.ToString();
                }

                switch (sysmanLogMessageEntries.Max(p => (int)p.Severity))
                {
                    case 0: return SysmanLogResult.Completed.ToString();
                    case 1: return SysmanLogResult.CompletedWithWarnings.ToString();
                    case 2: return SysmanLogResult.Failed.ToString();
                    default: return SysmanLogResult.None.ToString();
                }
            }
        }
        public string Text
        {
            get
            {
                string result = string.Empty;
                foreach (var entry in sysmanLogMessageEntries)
                {
                    result += $"[{entry.Severity.ToString().ToUpper()}] {entry.DateTime.ToString(dateFormat)} {entry.Message}\n";
                }
                return result.Trim(new[] { '\n' });
            }
        }

        internal SysmanLogMessage(string source, string method, string version, long? actionId)
        {
            if (string.IsNullOrEmpty(source)) { throw new ArgumentNullException("source"); }
            if (string.IsNullOrEmpty(method)) { throw new ArgumentNullException("method"); }
            if (string.IsNullOrEmpty(version)) { throw new ArgumentNullException("version"); }

            Source = source;
            Method = method;
            MethodVersion = version;
            ActionId = actionId;
        }

        internal void AddEntry(string message, SysmanLogSeverity severity)
        {
            DateTime executionTime = DateTime.Now;
            if (message.Contains("\r\n"))
            {
                message = message.Replace("\r\n", "\n");
            }
            if (message.Contains("\r"))
            {
                message = message.Replace("\r", "\n");
            }
            foreach (string part in message.Split(new char['\n']))
            {
                sysmanLogMessageEntries.Add(new SysmanLogMessageEntry(part, severity));
            }
        }

        public void AddVerboseEntry(string message)
        {
            AddEntry(message, SysmanLogSeverity.Verbose);
        }

        public void AddWarningEntry(string message)
        {
            AddEntry(message, SysmanLogSeverity.Warning);
        }

        public void AddErrorEntry(string message)
        {
            AddEntry(message, SysmanLogSeverity.Error);
        }

        public class SysmanLogMessageEntry
        {
            public string Message { get; private set; }
            public SysmanLogSeverity Severity { get; private set; }
            public DateTime DateTime { get; private set; }

            internal SysmanLogMessageEntry(string message, SysmanLogSeverity severity)
            {
                Message = message;
                Severity = severity;
                DateTime = DateTime.Now;
            }
        }
    }
}
