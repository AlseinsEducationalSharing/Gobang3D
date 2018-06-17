using System.Runtime.CompilerServices;
using System.Threading.Tasks;

internal class Resulter
{
    private TaskCompletionSource<object> _source = new TaskCompletionSource<object>();

    private async Task GetTask() => await _source.Task;

    public TaskAwaiter GetAwaiter() => GetTask().GetAwaiter();

    public void Result()
    {
        var oldSource = _source;
        _source = new TaskCompletionSource<object>();
        oldSource.SetResult(null);
    }
}
