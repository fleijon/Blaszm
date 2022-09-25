namespace Blaszm.Features.Extensions;

public static class TaskExtensions
{
    public static async Task RunSafe(this Task taskCall, Action<Exception>? onFailCallback = null)
    {
        try
        {
            await taskCall;
        }
        catch (Exception ex)
        {
            onFailCallback?.Invoke(ex);
        }
    }
}
