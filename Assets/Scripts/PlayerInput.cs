using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Griffin Geiger

[System.Serializable]
public class PlayerInput
{
    public enum playerTag { None = 0,  Player1 = 1 , Player2 =2 , Player3 = 3 , Player4 = 4 };
    public readonly playerTag m_playerTag; 
    public PlayerControlledObjects m_controller;
    public bool allowCharacterMovement;
    public float m_triggerPullThreshold=.5f;

    private Vector2 m_leftJoystickMovement= new Vector2();
    private Vector2 m_rightJoystickMovement = new Vector2();
    private bool m_interactionButton;
    private bool m_throwButton;


    public PlayerInput(int playerNumber)
    {
        m_playerTag = (playerTag) playerNumber;
    }


    // Update is called once per frame
    public void Update()
    {
        m_leftJoystickMovement.x = Input.GetAxisRaw( m_playerTag + "_Left_Stick_x");
        m_leftJoystickMovement.y = Input.GetAxisRaw( m_playerTag + "_Left_Stick_y");
        m_rightJoystickMovement.x = Input.GetAxisRaw(m_playerTag + "_Right_Stick_x");
        m_rightJoystickMovement.y = Input.GetAxisRaw(m_playerTag + "_Right_Stick_y");
        m_interactionButton     = Input.GetButtonDown( m_playerTag + "_A_Button");
        m_throwButton = Input.GetAxisRaw(m_playerTag + "_Right_Trigger") >= m_triggerPullThreshold;
        Debug.Log("stick:" + m_rightJoystickMovement);

    }

    public void FixedUpdate()
    {
        //might need to check state of game here
        if (allowCharacterMovement)
        {
            if (m_controller != null)
            {
                m_controller.Move(m_leftJoystickMovement); //move character
                m_controller.Aim(m_rightJoystickMovement);

                if (m_interactionButton)
                {
                    if (m_controller is CharacterMovement)
                    {
                        ((CharacterMovement)m_controller).Interact();
                    }
                    else if (m_controller is DraggableObject)
                    {
                        ((DraggableObject)m_controller).Place();
                    }

                    m_interactionButton = false;
                }

                if(m_throwButton)
                {
                   if(m_controller is CharacterMovement)
                    {
                        Debug.Log("m_controller is CharacterMovement and throwing");
                        ((CharacterMovement) m_controller).Throw();
                    }

                    m_throwButton = false;
                }
            }
            else
                Debug.LogError("PlayerInput" + m_playerTag + " does not have a reference to CharacterMovement instance but is allowing movement.");
        }
    }

}
