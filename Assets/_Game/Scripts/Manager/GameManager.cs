using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    MainMenu = 0,
    GamePlay = 1,
    Setting = 2,
    Finish = 3,
    Revive = 4,
}
public class GameManager : Singleton<GameManager>
{
    private GameState state;

    public void ChangeStage(GameState gameState)
    {
        state = gameState;
    }

    public bool IsStage(GameState gameState)
    {
        return state == gameState;
    }
}
