﻿using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient
{
    public class PollyRetry
    {
        public RetryPolicy RetryPolicy = Policy
            .Handle<Exception>()
            .Retry(3, onRetry: (e, retryAttempt) =>
            {
                Console.WriteLine($"> Retry Attempt {retryAttempt}");
            });

        public RetryPolicy WaitAndRetryPolicy = Policy
              .Handle<Exception>()
              .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
              onRetry: (exception, timespan, retryAttempt, context) =>
              {
                  Console.WriteLine($"> Retry Attempt {retryAttempt}");
                  Console.WriteLine($"> Waiting for {timespan.TotalSeconds} seconds...");
              });


        public void Retry()
        {
            Console.WriteLine("> Policy: Retry with 3 attempt");

            RetryPolicy.Execute(() =>
            {
                ColoredConsole.WriteBlue("> Calling WebService...");
                var result = new ClientService().GetSomeThing();
                ColoredConsole.WriteGreen($"> Success: {result}");
            });
        }

        public void WaitAndRetryExponential()
        {
            Console.WriteLine("> Policy: Retry with 3 attempt and exponential backoff");

            WaitAndRetryPolicy.Execute(() =>
            {
                ColoredConsole.WriteBlue("> Calling WebService...");
                var result = new ClientService().GetSomeThing();
                ColoredConsole.WriteGreen($"> Success: {result}");
            });
        }
    }
}
