using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient.Common
{
    public class PolicyTest
    {
        public PolicyTest(string policyName, Action action)
        {
            PolicyName = policyName;
            Action = action;
        }

        public string PolicyName { get; private set; }
        public Action Action { get; private set; }
    }
}
