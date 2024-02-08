using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState
{
    public Alien alien;
    public StateMachine stateMachine;

    // Set up game properties 
    public abstract void Enter();
    // Called every frame for real-time logic
    public abstract void Perform();
    // Process cleanup
    public abstract void Exit();
}
