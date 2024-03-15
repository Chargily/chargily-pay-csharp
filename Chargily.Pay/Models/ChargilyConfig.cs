namespace Chargily.Pay.Models;

public record ChargilyConfig
{
    public bool IsLiveMode { get; set; } = false;
    public string ApiSecretKey { get; set; }
    public bool EnableCache { get; set; } = true;
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromSeconds(30);
    public int MaxRetriesOnFailure { get; set; } = 10;

    public Func<int, TimeSpan>? DelayPerRetryCalculator { get; set; }
        = (attempt) => TimeSpan.FromMilliseconds(Math.Clamp(attempt * 500, 0, 30_000));

    internal TimeSpan GetCacheDuration() => EnableCache ? CacheDuration : TimeSpan.Zero;
    public TimeSpan BalanceRefreshInterval { get; set; } = TimeSpan.FromSeconds(30);
    public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(120);
    public TimeSpan TooManyRequestsBackOffDuration { get; set; } = TimeSpan.FromSeconds(15);
};