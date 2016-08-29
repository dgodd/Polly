﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Polly.Shared
{
    class MetricStream
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static IEnumerable<object> All(Action sleepFunc)
        {
            while (true)
            {
                foreach (var policy in CollectedPolicies.All)
                {
                    yield return new HystrixCommand
                    {
                        rollingCountSuccess = policy.Value.HealthCount.Successes,
                        errorCount = policy.Value.HealthCount.Failures,
                        isCircuitBreakerOpen = (policy.Value.CircuitState != CircuitBreaker.CircuitState.Closed),
                        type = "HystrixCommand",
                        name = policy.Key,
                        group = "Order",
                        currentTime = (long)((DateTime.UtcNow - Jan1St1970).TotalMilliseconds), // 1471353864601,
                        latencyExecute = new Dictionary<string, int>() { { "0", 0 }, { "25", 0 }, { "50", 0 }, { "75", 0 }, { "90", 0 }, { "95", 0 }, { "99", 0 }, { "99.5", 0 }, { "100", 0 } },
                        latencyTotal = new Dictionary<string, int>() { { "0", 0 }, { "25", 0 }, { "50", 0 }, { "75", 0 }, { "90", 0 }, { "95", 0 }, { "99", 0 }, { "99.5", 0 }, { "100", 0 } },
                        propertyValue_executionIsolationStrategy = "THREAD",
                        threadPool = "Order"
                    };
                }

                // yield return new HystrixThreadPool { type = "HystrixThreadPool", name = "Order" };

                sleepFunc.Invoke();
            }
        }
    }
}
