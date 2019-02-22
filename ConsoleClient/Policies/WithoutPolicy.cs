using System;
using System.Collections.Generic;
using System.Text;
using ConsoleClient.Services;

namespace ConsoleClient.Policies
{
    public class WithoutPolicy
    {
        public void Run()
        {
            Console.WriteLine("Run WithoutPolicy");
            try
            {
                var result = new ClientService().GetSomeThing();
                ColoredConsole.WriteGreen($"Success: {result}");
            }
            catch(Exception ex)
            {
                ColoredConsole.WriteRed($"Fail: {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}
