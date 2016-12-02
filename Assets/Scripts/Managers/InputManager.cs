using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using CnControls;

public class InputManager : PersistentSingleton<InputManager>
{

    public event EventHandler<AxisEventArgs> OnMoveAxisChange
    {
        add
        {
            _onMoveAxisChanged += value;
        }
        remove
        {
            _onMoveAxisChanged -= value;
        }
    }
    public event EventHandler<AxisEventArgs> OnAimAxisChange
    {
        add
        {
            _onAimAxisChanged += value;
        }
        remove
        {
            _onAimAxisChanged -= value;
        }
    }
    public event EventHandler OnFireClick
    {
        add
        {
            _onFireClicked += value;
        }
        remove
        {
            _onFireClicked -= value;
        }
    }
    private event EventHandler<AxisEventArgs> _onMoveAxisChanged;
    private event EventHandler<AxisEventArgs> _onAimAxisChanged;
    private event EventHandler _onFireClicked;


    void Update()
    {

        float aimAxisY = CnInputManager.GetAxis("Vertical");
        float aimAxisX = CnInputManager.GetAxis("Horizontal");
        float axisMoveX = CnInputManager.GetAxis("AxisMoveX");
        float axisMoveY = CnInputManager.GetAxis("AxisMoveY");


        if (_onAimAxisChanged != null)
            _onAimAxisChanged(this, new AxisEventArgs(new Vector2(aimAxisX, aimAxisY)));
        
        if (_onMoveAxisChanged != null)
            _onMoveAxisChanged(this, new AxisEventArgs(new Vector2(axisMoveX, axisMoveY)));

        if (CnInputManager.GetButtonDown("Jump"))
        {
            if (_onFireClicked != null)
                _onFireClicked(this, null);
        }
         
    }


}