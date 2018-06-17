using System.Runtime.CompilerServices;
using System.Threading.Tasks;

internal class Resulter
{
    private TaskCompletionSource<object> source = new TaskCompletionSource<object>();

    private async Task GetTask() => await source.Task;

    public TaskAwaiter GetAwaiter() => GetTask().GetAwaiter();

    public void Result()
    {
        var oldSource = source;
        source = new TaskCompletionSource<object>();
        oldSource.SetResult(null);
    }
}
