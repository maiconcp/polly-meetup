using System;
using System.Collections.Generic;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Polly Client Started...");

            new PollyConsole().Run();           

            Console.ReadLine();
        }


    }

    public class PolicyTest
    {
        public PolicyTest(string policyName, Action action)
        {
            PolicyName = policyName;
            Action = action;
        }

        public string PolicyName { get; private set; }
        public Action Action { get; private set; }
    }
}
