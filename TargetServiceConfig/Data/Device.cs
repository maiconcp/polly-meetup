using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TargetServiceConfig.Data
{
    public class Device
    {
        public bool Enabled { get; private set; }
        public int Timeout { get; private set; }

        public string CurrentLog => Logger.ToString();

        private readonly static TextWriter Logger = new StringWriter();
        private static readonly Device instance = new Device();

        static Device()
        {
            Console.SetOut(Logger);
        }

        private Device()
        {
        }

        public static Device Instance
        {
            get
            {
                return instance;
            }
        }

        public void On(int timeout = 0)
        {
            Enabled = true;
            Timeout = timeout;
        }

        public void Off(int timeout = 0)
        {
            Enabled = false;
            Timeout = timeout;
        }
    }
}
