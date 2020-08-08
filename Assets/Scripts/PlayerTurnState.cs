using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : GameState
{
    public bool IsPlayerRolling { get; set; }

    public PlayerTurnState(GameManager gameManager) 
        : base(gameManager)
    {
    }

    public override void Start()
    {
        Debug.Log("Player turn begin.");        
    }

    public override void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player turn rolling dice.");

            GameManager.IsPlayerRolling = true;
            GameManager.RollDice();
            GameManager.SetState(new ValidateRollState(GameManager));
        }
    }
}
