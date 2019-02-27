using System;
using System.Collections.Generic;
using System.Text;
using ConsoleClient.Services;
using Polly;
using Polly.Timeout;

namespace ConsoleClient.Policies
{
    public class PollyTimeout
    {
        public readonly TimeoutPolicy TimeoutPolicy = Policy.Timeout(2, TimeoutStrategy.Pessimistic, onTimeout: (context, timespan, task) =>
        {
            Console.WriteLine("Timeout!");
        });

        public void Timeout()
        {
            Console.WriteLine("> Policy: Timeout 2s - Pessimistic");

            try
            {
                TimeoutPolicy.Execute(() =>
                {
                    ColoredConsole.WriteBlue("> Calling WebService...");
                    var result = new ClientService().GetSomeThing();
                    ColoredConsole.WriteGreen($"> Success: {result}");
                });
            }
            catch
            {
                ColoredConsole.WriteRed("Fail.");
            }

        }
    }
}
