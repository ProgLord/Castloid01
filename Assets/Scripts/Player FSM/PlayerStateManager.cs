using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.PlayerFSM;

public class PlayerStateManager : MonoBehaviour 
{
    private IStateBase activeState;

    private static PlayerStateManager instanceRef;

    private void Awake()
    {
        if(instanceRef == null)
        {
            instanceRef = this;
        }
    }

    void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    private void FixedUpdate()
    {
        activeState.StateFixedUpdate();
    }

    public void SwitchStates(IStateBase newState)
    {
        activeState = newState;
    }
}
