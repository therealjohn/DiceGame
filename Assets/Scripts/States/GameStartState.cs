using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : GameState
{
    public GameStartState(GameManager gameManager) 
        : base(gameManager)
    {       
    }

    public override void Start()
    {
        GameManager.ResetGame();
        GameManager.SetState(new PlayerTurnState(GameManager));
    }
}
