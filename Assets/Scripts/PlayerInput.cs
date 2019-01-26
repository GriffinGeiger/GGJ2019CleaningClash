using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Griffin Geiger

[System.Serializable]
public class PlayerInput
{
    public enum playerTag { Player1 = 1 , Player2 =2 , Player3 = 3 , Player4 = 4 };
    public readonly playerTag m_playerTag; 
    public CharacterMovement m_controller;
    public bool allowCharacterMovement;

    private Vector2 m_leftJoystickMovement= new Vector2();
    private Vector2 m_rightJoystickMovement = new Vector2();
    private bool m_interactionButton;


    public PlayerInput(int playerNumber)
    {
        m_playerTag = (playerTag) playerNumber;
    }


    // Update is called once per frame
    void Update()
    {
        m_leftJoystickMovement.x = Input.GetAxisRaw("Player" + m_playerTag + "_Joystick_x");
        m_leftJoystickMovement.y = Input.GetAxisRaw("Player" + m_playerTag + "_Joystick_y");
        m_rightJoystickMovement.x = Input.GetAxisRaw("Player" + m_playerTag + "_Joystick_x");
        m_rightJoystickMovement.y = Input.GetAxisRaw("Player" + m_playerTag + "_Joystick_y");
        m_interactionButton  = Input.GetButtonDown("Player" + m_playerTag + "_interaction"); 
    }

    private void FixedUpdate()
    {
        //might need to check state of game here
        if (allowCharacterMovement)
        {
            Debug.Log("Movement is allowed");
            if (m_controller != null)
            {
                m_controller.Move(m_leftJoystickMovement); //move character
                m_controller.Aim(m_rightJoystickMovement);

                if (m_interactionButton)
                {
                    m_controller.Interact();
                    m_interactionButton = false;
                }
            }
            else
                Debug.LogError("PlayerInput" + m_playerTag + " does not have a reference to CharacterMovement instance but is allowing movement.");
        }
    }

}
