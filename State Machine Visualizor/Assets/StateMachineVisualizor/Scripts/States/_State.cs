using System;

using UnityEngine;

public class State
{

    public event Action OnComplete;

    protected State parent;
    protected State[] children;


    public void Start()
    {

    }
    public void Enter()
    {

    }
    public void Update()
    {

    }
    public void FixedUpdate()
    {

    }
    public void Exit()
    {

    }


}
