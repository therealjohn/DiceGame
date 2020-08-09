using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    protected GameState state;

    public void SetState(GameState state)
    {
        this.state?.Exit();

        this.state = state;

        this.state?.Start();
    }
}
