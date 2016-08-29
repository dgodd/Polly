namespace Polly.CircuitBreaker
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IHealthCount
    {
        int Successes { get; }
        int Failures { get; }
        int Total { get; }
        long StartedAt { get; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    internal class HealthCount : IHealthCount
    {
        public int Successes { get; set; }

        public int Failures { get; set; }

        public int Total { get { return Successes + Failures; } }

        public long StartedAt { get; set; }
    }
}
