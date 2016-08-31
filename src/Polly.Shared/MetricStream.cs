using System;
using System.Collections.Generic;
using System.Text;

namespace Polly.Shared
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MetricStream
    {
        public static IEnumerable<object> All(Action sleepFunc)
        {
            while (true)
            {
                foreach (var policy in CollectedPolicies.All)
                {
                    yield return new HystrixCommand
                    {
                        rollingCountSuccess = policy.Value.HealthCount.Successes,
                        rollingCountFailure = policy.Value.HealthCount.Failures,
                        isCircuitBreakerOpen = (policy.Value.CircuitState != CircuitBreaker.CircuitState.Closed),
                        name = policy.Key,
                        group = "Group",
                        latencyExecute = new Dictionary<string, int>() { { "0", 0 }, { "25", 0 }, { "50", 0 }, { "75", 0 }, { "90", 0 }, { "95", 0 }, { "99", 0 }, { "99.5", 0 }, { "100", 0 } },
                        latencyTotal = new Dictionary<string, int>() { { "0", 0 }, { "25", 0 }, { "50", 0 }, { "75", 0 }, { "90", 0 }, { "95", 0 }, { "99", 0 }, { "99.5", 0 }, { "100", 0 } },
                        propertyValue_executionIsolationStrategy = "THREAD",
                        threadPool = "ThreadPool"
                    };
                }

                // yield return new HystrixThreadPool { type = "HystrixThreadPool", name = "Order" };

                sleepFunc.Invoke();
            }
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
