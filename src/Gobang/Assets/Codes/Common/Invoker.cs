using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Invoker<T>
{
    public Invoker(T Target)
    {
        this.Target = Target;
        PendingInvokes = new Queue<Action<T>>();
    }

    private T Target;
    private Queue<Action<T>> PendingInvokes;

    public void Invoke(Action<T> act, bool async = false)
    {
        lock (PendingInvokes)
        {
            PendingInvokes.Enqueue(act);
        }
        while (!async)
        {
            lock (PendingInvokes)
            {
                if (!PendingInvokes.Contains(act))
                {
                    break;
                }
            }
        }
    }

    public void DispatchInvoke()
    {
        while (true)
        {
            lock (PendingInvokes)
            {
                if (PendingInvokes.Count == 0)
                {
                    break;
                }
                PendingInvokes.Dequeue()(Target);
            }
        }
    }
}