using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Polly.Shared
{
    public class CollectedPolicies
    {
        private static WeakDictionary<string, CircuitBreakerPolicy> _policies;
        static CollectedPolicies() {
            _policies = new WeakDictionary<string, CircuitBreakerPolicy>();
        }

        public static IEnumerable<KeyValuePair<string, CircuitBreakerPolicy>> All
        {
            get { return _policies.All(); }
        }

        public static void Add(string name, CircuitBreakerPolicy policy)
        {
            _policies.Add(name, policy);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
