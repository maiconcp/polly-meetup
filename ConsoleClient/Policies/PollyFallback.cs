using System;
using System.Collections.Generic;
using System.Text;
using ConsoleClient.Services;
using Polly;
using Polly.Fallback;

namespace ConsoleClient.Policies
{
    public class PollyFallback
    {
        public readonly FallbackPolicy<string> FallbackPolicy = Policy<string>
                                                            .Handle<Exception>()
                                                            .Fallback(() => new ClientAlternativeService().GetSomeThing(), onFallback: (a) =>
                                                            {
                                                                ColoredConsole.WriteBlue("> Calling Alternative WebService...");
                                                            });
            

        public void Fallback()
        {
            Console.WriteLine("> Policy: Fallback: Get in another service");

            ColoredConsole.WriteBlue("> Calling main WebService...");
            string result = FallbackPolicy.Execute(() =>
            {
                try
                {
                    string serviceResult = new ClientService().GetSomeThing();
                    ColoredConsole.WriteGreen($"> Success: {serviceResult}");
                    return serviceResult;
                }
                catch
                {
                    ColoredConsole.WriteRed("Main Webservice Failed!");
                    throw;
                }
            });

            Console.WriteLine(result);
        }
    }
    
}
