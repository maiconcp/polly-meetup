using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsoleClient.Services;
using Polly;
using Polly.Bulkhead;

namespace ConsoleClient.Policies
{
    public class PollyBulkhead
    {
        public readonly AsyncBulkheadPolicy<string> BulkheadPolicy = Policy.BulkheadAsync<string>(5, onBulkheadRejectedAsync: (context) =>
        {
            return Task.Run(() => Console.WriteLine("Request rejected!"));
        });

        public void Bulkhead()
        {
            ColoredConsole.WriteWhite("> Policy: Bulkhead with 5 slot (and 10 calls)");
            
            Parallel.For(1, 11, async (i) =>
            {
                await BulkheadAsync(i);
            });
        }

        public async Task<string> BulkheadAsync(int i)
        {
            try
            {
                await Task.Delay(i * 200);

                return await BulkheadPolicy.ExecuteAsync(async () =>
                {
                    ColoredConsole.WriteWhite($"Request {i}. BulkheadAvailableCount: {BulkheadPolicy.BulkheadAvailableCount}");
                    
                    ColoredConsole.WriteBlue("> Calling WebService...");
                    var result = await new ClientService().GetSomeThingAsync();
                    ColoredConsole.WriteGreen($"> Success: {result}");
                    
                    return result;
                });
            }
            catch(BulkheadRejectedException ex)
            {
                ColoredConsole.WriteRed($"Request {i} rejected: {ex.Message}");

                return "Rejected.";
            }
            catch (Exception ex)
            {
                ColoredConsole.WriteRed($"Request {i} failed: {ex.Message}");

                return "Fail result.";
            }
        }
    }
}
