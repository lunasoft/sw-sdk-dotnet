using System;
using System.Threading.Tasks;

namespace SW.Helpers
{
    public static class RetryHelper
    {
        public static T Retry<T>(Func<T> action, int maxRetries, int retryIntervalSeconds = 1)
        {
            for (int numRetries = 0; numRetries < maxRetries; numRetries++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    if (numRetries == maxRetries - 1)
                    {
                        throw new Exception($"({DateTime.Now}) Failed after {maxRetries} retries, last error: {ex.Message} {ex.InnerException?.Message ?? ""}.", ex);
                    }
                    Task.Delay(TimeSpan.FromSeconds(retryIntervalSeconds));
                }
            }

            return default(T);
        }
    }
}
