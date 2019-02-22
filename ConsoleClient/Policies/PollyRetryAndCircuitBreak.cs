using ConsoleClient.Services;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient.Policies
{
    public class PollyRetryAndCircuitBreak
    {
        public void CircuitBreak()
        {
            Console.WriteLine("> Policy: Retry with 3 attempt and exponential backoff");
            Console.WriteLine("> Policy: CircuitBreak and Break the circuit after 2 consecutive exceptions");

            var circuitBreak = new PollyCircuitBreak().CircuitBreakerPolicy;
            var retry = new PollyRetry().WaitAndRetryPolicy;

            string option = string.Empty;
            do
            {
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                Console.Write("[C] Call   [E] Exit");
                option = Console.ReadKey().KeyChar.ToString().ToUpper();
                Console.WriteLine();

                if (option.ToUpper().Equals("C"))
                {
                    try
                    {
                        circuitBreak
                            .Wrap(retry)
                            .Execute(() =>
                            {
                                ColoredConsole.WriteBlue("> Calling WebService...");
                                var result = new ClientService().GetSomeThing();
                                ColoredConsole.WriteGreen($"> Success: {result}");
                            });
                    }
                    catch (Exception)
                    {
                        ColoredConsole.WriteRed("> Fail!");
                    }

                    Console.WriteLine();
                }
                Console.WriteLine($"> Circuit Break State: {circuitBreak.CircuitState}");
            }
            while (option != "E");
        }
    }
}
