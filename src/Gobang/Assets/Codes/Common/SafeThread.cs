using System;
using System.Collections.Generic;
using System.Threading;

class SafeThread : IDisposable
{
    private static Dictionary<int, WeakReference> SafeThreads;

    static SafeThread()
    {
        SafeThreads = new Dictionary<int, WeakReference>();
    }

    public static void HandleAbort()
    {
        var id = Thread.CurrentThread.ManagedThreadId;
        if (SafeThreads.ContainsKey(id) && (SafeThreads[id].Target as SafeThread).Exiting)
        {
            throw new ThreadAbortException();
        }
    }

    private Thread WorkThread;
    private bool Exiting;

    public SafeThread(Action WorkMethod)
    {
        WorkThread = new Thread(() =>
        {
            try
            {
                WorkMethod();
            }
            catch (ThreadAbortException)
            {
            }
        });
        WorkThread.IsBackground = true;
        SafeThreads.Add(WorkThread.ManagedThreadId, new WeakReference(this));
    }

    public void Start()
    {
        WorkThread.Start();
    }

    ~SafeThread()
    {
        Dispose(false);
    }

    private class ThreadAbortException : Exception { }

    #region IDisposable Support
    private bool disposedValue = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
            var id = WorkThread.ManagedThreadId;
            Exiting = true;
            WorkThread.Join();
            SafeThreads.Remove(id);
            disposedValue = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
    }
    #endregion
}
