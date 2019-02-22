using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient
{
    public class PollyRetryAndCircuitBreakExceptionBased
    {
        private CircuitBreakerPolicy circuitBreakerPolicy = Polly.Policy
                                      .Handle<Exception>()
                                      .CircuitBreaker(2, TimeSpan.FromSeconds(10),
                                      onBreak: (exception, timespan) =>
                                      {
                                          Console.WriteLine($"break, will open in {timespan.TotalSeconds}s");
                                      },
                                      onReset: () =>
                                      {
                                          Console.WriteLine("reset...");
                                      },
                                      onHalfOpen: () =>
                                      {
                                          Console.WriteLine("in HalfOpen");
                                      });


        private RetryPolicy retryPolicy = Policy
                                  .Handle<Exception>()
                                  .WaitAndRetry(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                  onRetry: (exception, timespan) =>
                                  {
                                      Console.WriteLine($"Waiting for {timespan.TotalSeconds} seconds...");
                                  });

        public void CircuitBreak()
        {
            Console.WriteLine("Break the circuit after 2 consecutive exceptions");
            string option = string.Empty;
            do
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("[S] Call with success         [F] Call with fail          [E] Exit");
                option = Console.ReadKey().KeyChar.ToString().ToUpper();
                Console.WriteLine();

                if ((option == "S") || (option == "F"))
                {
                    try
                    {
                        CircuitBreak(success: option == "S");
                        ColoredConsole.WriteGreen("Success!");
                    }
                    catch (Exception)
                    {
                        ColoredConsole.WriteRed("Fail!");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"--> Circuit Break State: {circuitBreakerPolicy.CircuitState}");
            }
            while (option != "E");
        }

        private void CircuitBreak(bool success)
        {
            retryPolicy.Wrap(circuitBreakerPolicy)
            .Execute(() =>
            {
                ColoredConsole.WriteBlue("Calling WebService...");

                if (!success)
                    throw new Exception("Fail");
            });
            
        }
    }
}
