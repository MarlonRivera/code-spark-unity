using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerState
{
    ALIVE,
    DEAD,
}

public class PlayerStateManager : MonoBehaviour
{
    private static PlayerStateManager instance;

    public PlayerState currentState;
    public PlayerState previousState;
    public static PlayerStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerStateManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("PlayerStateManager");
                    instance = singletonObject.AddComponent<PlayerStateManager>();
                }
            }
            DontDestroyOnLoad(instance.gameObject);
            return instance;
        }
    }

    private void Start()
    {
        currentState = PlayerState.ALIVE;
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    public PlayerState GetPreviousState()
    {
        return previousState;
    }

    public void SetCurrentSate(PlayerState state)
    {
        previousState = currentState;
        currentState = state;
    }

    public void ChangeState(PlayerState newState, System.Action callbackNavigation)
    {
        if (callbackNavigation != null)
        {
            SetCurrentSate(newState);
            callbackNavigation(); 
        }
    }
}
