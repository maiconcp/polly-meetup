using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient
{
    public class PollyCircuitBreak
    {
        private CircuitBreakerPolicy Policy = Polly.Policy
                                      .Handle<Exception>()
                                      .CircuitBreaker(2, TimeSpan.FromSeconds(10),
                                      onBreak: (exception, timespan) =>
                                      {
                                          Console.WriteLine($"> break, will open in {timespan.TotalSeconds}s");
                                      },
                                      onReset: () =>
                                      {
                                          Console.WriteLine("> reset...");
                                      },
                                      onHalfOpen: () =>
                                      {
                                          Console.WriteLine("> in HalfOpen");
                                      });



        public void CircuitBreak()
        {
            Console.WriteLine("> Policy: CircuitBreak and Break the circuit after 2 consecutive exceptions");

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
                        Policy.Execute(() =>
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
                Console.WriteLine($"> Circuit Break State: {Policy.CircuitState}");
            }
            while (option != "E");
        }

    }
}
