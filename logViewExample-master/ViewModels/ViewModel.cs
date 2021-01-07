using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using LogViewExample.Models;

namespace LogViewExample.ViewModels
{
    public class ViewModel
    {
        private const string TEST_DATA =
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

        private readonly List<string> _words;
        private readonly int _maxword;
        private int _index;

        public ObservableCollection<LogEntry> LogEntries { get; set; }
        private readonly Random _random;
        public Timer Timer { get; }

        public ViewModel()
        {
            _random = new Random();
            _words = TEST_DATA.Split(' ').ToList();
            _maxword = _words.Count - 1;

            LogEntries = new ObservableCollection<LogEntry>();

            Enumerable.Range(0, 200000)
                .ToList()
                .ForEach(x => LogEntries.Add(GetRandomEntry()));

            Timer = new Timer(x => AddRandomEntry(), null, 1000, 10);
        }

        /// <summary>
        /// Add random log entry to the ViewList evoked by a timer.
        /// </summary>
        private void AddRandomEntry()
        {
            if (Application.Current != null)
                Application.Current.Dispatcher.BeginInvoke((Action) (() => LogEntries.Add(GetRandomEntry())));
        }

        /// <summary>
        /// Generate log entries and sub entries.
        /// </summary>
        /// <returns>LogEntry object.</returns>
        private LogEntry GetRandomEntry()
        {
            if (_random.Next(1, 10) > 1)
            {
                return new LogEntry()
                {
                    Index = _index++,
                    DateTime = DateTime.Now,
                    Message = string.Join(" ", Enumerable.Range(5, _random.Next(10, 50))
                        .Select(x => _words[_random.Next(0, _maxword)])),
                };
            }

            return new CollapsibleLogEntry()
            {
                Index = _index++,
                DateTime = DateTime.Now,
                Message = string.Join(" ", Enumerable.Range(5, _random.Next(10, 50))
                    .Select(x => _words[_random.Next(0, _maxword)])),
                Contents = Enumerable.Range(5, _random.Next(5, 10))
                    .Select(i => GetRandomEntry())
                    .ToList()
            };
        }
    }
}
