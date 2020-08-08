using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    protected GameManager GameManager;

    public GameState(GameManager gameManager)
    {
        GameManager = gameManager;
    }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void Exit() { }
}
