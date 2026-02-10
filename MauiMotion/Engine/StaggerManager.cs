namespace MauiMotion.Engine;

internal static class StaggerManager
{
    private static int _sequenceIndex;
    private static CancellationTokenSource? _resetTokenSource;
        
    // If no new items appear for 150ms, we assume the "batch" is done and reset.
    private const int BatchResetTimeMs = 150;

    internal static int GetNextDelay(int staggerMs)
    {
        // 1. Cancel any pending reset. The "Burst" is still happening!
        _resetTokenSource?.Cancel();
        _resetTokenSource = new CancellationTokenSource();

        // 2. Calculate delay based on current index
        var delay = _sequenceIndex * staggerMs;
            
        // 3. Increment for the next item
        _sequenceIndex++;

        // 4. Schedule a reset. If no other item calls this within 150ms, 
        //    we reset the index back to 0 (ready for next scroll interaction).
        var token = _resetTokenSource.Token;
        Task.Delay(BatchResetTimeMs, token).ContinueWith(t =>
        {
            if (!t.IsCanceled)
            {
                _sequenceIndex = 0;
            }
        }, token);

        return delay;
    }
}