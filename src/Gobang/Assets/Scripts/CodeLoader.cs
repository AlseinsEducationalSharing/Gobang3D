using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

[Serializable]
public class CodeLoader : MonoBehaviour
{
    private SafeThread GameThread;
    public GameObject Target;

    void Start()
    {
        UserIO.MapInvoker = new Invoker<GameObject>(Target.transform.GetChild(0).gameObject);
        GameThread = new SafeThread(GameStart);
        GameThread.Start();
    }

    void GameStart()
    {
        while (true)
        {
            GameController.Current = new GobangGameController();
            GameController.Current.Start();
        }
    }

    private void Update()
    {
        UserIO.MapInvoker.DispatchInvoke();
    }

    void OnApplicationQuit()
    {
        GameThread.Dispose();
    }
}
