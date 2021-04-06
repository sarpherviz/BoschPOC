using System;
namespace Bosch.Consumer.Configuration
{
    public class RabbitMqConfiguration
    {
        public string Uri { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ushort PrefetchCount { get; set; }

        public int ConcurrentConsumerLimit { get; set; }

        public RateLimiter RateLimiter { get; set; }

        public IncrementalRetryPolicy IncrementalRetryPolicy { get; set; }

        public CircuitBreaker CircuitBreaker { get; set; }
    }

    public class RateLimiter
    {
        public int Limit { get; set; }

        public double IntervalSeconds { get; set; }
    }

    public class IncrementalRetryPolicy
    {
        public int RetryLimit { get; set; }

        public double InitialIntervalTime { get; set; }

        public double IntervalIncrementTime { get; set; }
    }

    public class CircuitBreaker
    {
        public int TripThreshold { get; set; }

        public int ActiveThreshold { get; set; }

        public double ResetInterval { get; set; }
    }
}
