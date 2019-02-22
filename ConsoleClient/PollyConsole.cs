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

            Policies = new Dictionary<string, PolicyTest>()
            {
                { "1" , new PolicyTest("Without Policy", () => new WithoutPolicy().Run()) },
                { "2" , new PolicyTest("Retry", () => retry.Retry()) },
                { "3" , new PolicyTest("WaitAndRetryExponential", () => retry.WaitAndRetryExponential()) },
                { "4", new PolicyTest("CircuitBreak", () => circuitBreak.CircuitBreak()) },
                { "5", new PolicyTest("RetryCircuitBreak", () => retryCircuitBreak.CircuitBreak()) }
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
            Console.WriteLine("=====================================================================");
            Console.WriteLine("Opções");
            foreach (var item in Policies)
            {
                Console.WriteLine($" {item.Key} - {item.Value.PolicyName}");
            }
            Console.WriteLine();
            Console.WriteLine("Escolha: ");
        }
    }
}
