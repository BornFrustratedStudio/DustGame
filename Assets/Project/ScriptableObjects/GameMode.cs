using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModeCondiction
{
    Idle,
    GameOver,
    GameWin
}

public abstract class GameMode : ScriptableObject
{
    public abstract void Enable();
    public abstract GameModeCondiction CheckCondiction(); 
    public abstract void Disable();
}
