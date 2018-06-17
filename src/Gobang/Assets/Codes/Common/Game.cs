using System.Threading.Tasks;
using UnityEngine;

internal class Game
{
    public UserIO UserIO { get; }
    public GameObject Chessboard { get; }
    public GameObject MessageBox { get; }

    public Game(GameObject chessboard, GameObject messageBox)
    {
        Chessboard = chessboard;
        MessageBox = messageBox;
        UserIO = new UserIO(this);
    }

    public Task ShowMessage(string message) => MessageBox.GetComponent<MessageBox>().Show(message);
}