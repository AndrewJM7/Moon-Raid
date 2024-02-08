using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    public void Initialise()
    {
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null)
        {
            // Run cleanup
            activeState.Exit();
        }
        // Change into new state
        activeState = newState;

        if (activeState != null)
        {
            activeState.stateMachine = this;
            activeState.alien = GetComponent<Alien>();
            activeState.Enter();
        }
    }
}
