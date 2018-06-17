using System;
using UnityEngine;

[Serializable]
public class GameEntrance : MonoBehaviour
{
    public GameObject Chessboard;
    public GameObject MessageBox;

    private Game _game;

    void Start()
    {
        _game = new Game(Chessboard, MessageBox);
        GameStart();
    }

    async void GameStart()
    {
        while (true)
        {
            await new GobangGameController(_game).Start();
        }
    }
}


