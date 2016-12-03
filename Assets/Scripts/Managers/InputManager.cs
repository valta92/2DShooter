using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using CnControls;

public class InputManager : PersistentSingleton<InputManager> , IInitialize
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


    private float aimAxisY;
    private float aimAxisX;

    private float moveAxisX;
    private float moveAxisY;

    public void Initialize()
    {
        
    }

    public void Disable()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.Paused)
            return;


        aimAxisY = CnInputManager.GetAxis("Vertical");
        aimAxisX = CnInputManager.GetAxis("Horizontal");
        moveAxisX = CnInputManager.GetAxis("AxisMoveX");
        moveAxisY = CnInputManager.GetAxis("AxisMoveY");


        if (_onAimAxisChanged != null)
            _onAimAxisChanged(this, new AxisEventArgs(new Vector2(aimAxisX, aimAxisY)));
        
        if (_onMoveAxisChanged != null)
            _onMoveAxisChanged(this, new AxisEventArgs(new Vector2(moveAxisX, moveAxisY)));

        if (CnInputManager.GetButtonDown("Jump"))
        {
            if (_onFireClicked != null)
                _onFireClicked(this, null);
        }
         
    }


}