using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public enum playerTag { Player1 = 1 , Player2 =2 , Player3 = 3 , Player4 = 4 };
    public playerTag m_playerTag; 
    public CharacterMovement m_controller;
    

    private Vector2 m_joystickMovement= new Vector2();
    private bool m_interactionButton;
    // Update is called once per frame
    void Update()
    {
        m_joystickMovement.x = Input.GetAxisRaw("Player" + m_playerTag + "_Joystick_x");
        m_joystickMovement.y = Input.GetAxisRaw("Player" + m_playerTag + "_Joystick_y");
        m_interactionButton  = Input.GetButtonDown("Player" + m_playerTag + "_interaction"); 
    }

    private void FixedUpdate()
    {
        //might need to check state of game here
        if(m_controller != null)
            m_controller.Move(m_joystickMovement); //move character

        if (m_interactionButton)
        {
            m_controller.Interact();
            m_interactionButton = false;
        }
    }
}
