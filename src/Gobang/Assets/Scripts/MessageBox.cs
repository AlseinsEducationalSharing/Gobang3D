using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private Resulter _resulter = new Resulter();

    public async Task Show(string message)
    {
        var text = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        text.text = $"<Color=#FFFFFF><Size=20>{message}</Size></Color>";
        gameObject.gameObject.SetActive(true);
        await _resulter;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        _resulter.Result();
    }
}
