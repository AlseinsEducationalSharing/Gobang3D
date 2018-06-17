using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private static Invoker<GameObject> invoker;
    public static void Show(string msg)
    {
        invoker.Invoke(Instance =>
        {
            var text = Instance.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            text.text = $"<Color=#FFFFFF><Size=20>{msg}</Size></Color>";
            Instance.gameObject.SetActive(true);
        });
    }

    void Start()
    {
        invoker = new Invoker<GameObject>(gameObject.transform.GetChild(0).gameObject);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        invoker.DispatchInvoke();
    }

    public void Close()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
