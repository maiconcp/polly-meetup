using ConsoleClient.Services;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching.Memory;
using Polly.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace ConsoleClient
{
    public class PollyCache
    {
        public CachePolicy<string> CachePolicy => Policy.Cache<string>(_memoryCacheProvider, TimeSpan.FromMinutes(5));
        private readonly MemoryCacheProvider _memoryCacheProvider;
        public PollyCache()
        {
            // This approach creates a CachePolicy directly, 
            // with its own Microsoft.Extensions.Caching.Memory.MemoryCache instance:
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            _memoryCacheProvider  = new MemoryCacheProvider(memoryCache);

        }

        public void Cache()
        {
            Console.WriteLine("> Policy: Cache");

            string option = string.Empty;
            do
            {
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                Console.Write("[0-9] Call Number   [E] Exit");
                option = Console.ReadKey().KeyChar.ToString().ToUpper();
                Console.WriteLine();

                if (!option.ToUpper().Equals("E"))
                {
                    try
                    {
                        var policyResult = CachePolicy.Execute(context =>
                        {
                            ColoredConsole.WriteBlue($"> Calling WebService getting item {option}...");
                            var result = new ClientService().GetSomeThing();

                            return result;
                        }, new Context(option));

                        ColoredConsole.WriteGreen($"> Success: {policyResult}");
                    }
                    catch (Exception)
                    {
                        ColoredConsole.WriteRed("> Fail!");
                    }

                    Console.WriteLine();
                }
            }
            while (option != "E");
        }
    }
}
