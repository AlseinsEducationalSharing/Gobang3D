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
            text.text = string.Format("<Color=#FFFFFF><Size=20>{0}</Size></Color>", msg);
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
