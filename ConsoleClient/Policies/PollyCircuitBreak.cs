using ConsoleClient.Services;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient.Policies
{
    public class PollyCircuitBreak
    {
        public CircuitBreakerPolicy CircuitBreakerPolicy => Policy
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
                        CircuitBreakerPolicy.Execute(() =>
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
                Console.WriteLine($"> Circuit Break State: {CircuitBreakerPolicy.CircuitState}");
            }
            while (option != "E");
        }

    }
}
