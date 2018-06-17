using System.Runtime.CompilerServices;
using System.Threading.Tasks;

internal class Resulter<T>
{
    private TaskCompletionSource<T> _source = new TaskCompletionSource<T>();

    public TaskAwaiter<T> GetAwaiter() => _source.Task.GetAwaiter();

    public void Result(T result)
    {
        var oldSource = _source;
        _source = new TaskCompletionSource<T>();
        oldSource.SetResult(result);
    }
}
