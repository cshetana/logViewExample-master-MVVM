﻿using System;
using System.Collections.Generic;

namespace LogViewExample.Models
{
    public class LogEntry
    {
        public DateTime DateTime { get; set; }
        public int Index { get; set; }
        public string Message { get; set; }
    }

    public class CollapsibleLogEntry : LogEntry
    {
        public List<LogEntry> Contents { get; set; }
    }
}