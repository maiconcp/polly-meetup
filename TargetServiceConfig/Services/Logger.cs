using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TargetServiceConfig.Services
{
    public class Logger
    {
        private Stack<string> _log = new Stack<string>();
        
        public void Add(string message)
        {
            _log.Push($"{DateTime.Now.ToString("HH:mm:ss.fff")}: {message}");
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _log.ToArray());
        }

        internal void Clear()
        {
            _log?.Clear();
        }
    }
}
