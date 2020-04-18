using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : Flow
{
    #region Singleton
    static private InputManager instance = null;

    static public InputManager Instance
    {
        get
        {
            return instance ?? (instance = new InputManager());
        }
    }
    #endregion

    public InputPkg inputs;

    override public void PreInitialize()
    {
        inputs = new InputPkg();
    }

    override public void Initialize()
    {
    }

    override public void Refresh()
    {

    }

    override public void PhysicsRefresh()
    {
       
    }

    override public void EndFlow()
    {
    }

    public class InputPkg
    {
     
    }
}

public class TouchController // TODO Check why can't use struct
{
    public Vector2 Joystick { get; set; }
    public bool JoystickPressed { get; set; }
    public bool NearPrimaryIndexTrigger { get; set; }
    public bool NearButtons { get; set; }
    public bool JoystickUp { get; set; }
    public bool JoystickDown { get; set; }
    public bool JoystickLeft { get; set; }
    public bool JoystickRight { get; set; }
    public bool ButtonOne { get; set; }
    public bool ButtonTwo { get; set; }
    public bool Start { get; set; }
    public float HandTrigger { get; set; }
    public float IndexTrigger { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
}

