using System.Runtime.CompilerServices;
using System.Threading.Tasks;

internal class Resulter<T>
{
    private TaskCompletionSource<T> source = new TaskCompletionSource<T>();

    public TaskAwaiter<T> GetAwaiter() => source.Task.GetAwaiter();

    public void Result(T result)
    {
        var oldSource = source;
        source = new TaskCompletionSource<T>();
        oldSource.SetResult(result);
    }
}
