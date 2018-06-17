using System;
using UnityEngine;

[Serializable]
public class GameEntrance : MonoBehaviour
{
    public GameObject Chessboard;
    public GameObject MessageBox;

    private Game _game;

    public void Start()
    {
        _game = new Game(Chessboard, MessageBox);
        GameStart();
    }

    private async void GameStart()
    {
        while (true)
        {
            await new GobangGameWorkflow(_game).Start();
        }
    }
}


