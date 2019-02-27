using System;
using System.Collections.Generic;
using System.Text;
using ConsoleClient.Common;
using ConsoleClient.Policies;

namespace ConsoleClient
{
    public class PollyConsole
    {
        private readonly Dictionary<string, PolicyTest> Policies;
        public PollyConsole()
        {
            var retry = new PollyRetry();
            var circuitBreak = new PollyCircuitBreak();
            var retryCircuitBreak = new PollyRetryAndCircuitBreak();
            var cache = new PollyCache();
            var fallback = new PollyFallback();
            var timeout = new PollyTimeout();
            var bulkhead = new PollyBulkhead();

            Policies = new Dictionary<string, PolicyTest>()
            {
                { "1" , new PolicyTest("Without Policy", () => new WithoutPolicy().Run()) },
                { "2" , new PolicyTest("Retry", () => retry.Retry()) },
                { "3" , new PolicyTest("Wait And Retry Exponential", () => retry.WaitAndRetryExponential()) },
                { "4", new PolicyTest("Circuit Breaker", () => circuitBreak.CircuitBreak()) },
                { "5", new PolicyTest("Retry + Circuit Breaker", () => retryCircuitBreak.RetryAndCircuitBreak()) },
                { "6", new PolicyTest("Cache", () => cache.Cache()) },
                { "7", new PolicyTest("Fallback", () => fallback.Fallback()) },
                { "8", new PolicyTest("Timeout", () => timeout.Timeout()) },
                { "9", new PolicyTest("Bulkhead Isolation", () => bulkhead.Bulkhead()) },
            };
        }

        public void Run()
        {
            string option = string.Empty;
            do
            {
                PrintOptions();
                
                option = Console.ReadLine();

                Console.Clear();

                RunOption(option);

                Console.WriteLine();
                Console.WriteLine("[Enter] to continue...");
                Console.ReadLine();

                Console.Clear();

            } while (true);
        }

        private void RunOption(string option)
        {
            if (!Policies.ContainsKey(option))
                Console.WriteLine("Opção não disponível.");
            else
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine($"> Executing {Policies[option].PolicyName}");
                    Policies[option].Action();
                }
                catch (Exception ex)
                {
                    ColoredConsole.WriteRed("> ");
                    ColoredConsole.WriteRed("Exception on Client: " + ex.Message);
                }
            }
        }

        private void PrintOptions()
        {
            ColoredConsole.WriteWhite("=====================================================================");
            ColoredConsole.WriteWhite("Opções");
            foreach (var item in Policies)
            {
                ColoredConsole.WriteWhite($" {item.Key} - {item.Value.PolicyName}");
            }
            Console.WriteLine();
            ColoredConsole.WriteWhite("Escolha: ");
        }
    }
}
