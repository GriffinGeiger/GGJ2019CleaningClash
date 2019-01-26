using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Griffin Geiger

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D m_rigidbody;
    public Transform m_aimArrowTransform;
    public PlayerInput.playerTag m_playerTag; //defines which player controls this character
    public float m_speedFactor;
    public float m_arrowDistance;

    private GameManager gm;

    private float m_stunTime = 0;
    private bool m_stunned = false;
    //public Item m_heldItem;
    private Vector2 m_aimVector;
    

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        ConnectToPlayerInput();
    }

    public void ConnectToPlayerInput()
    {
        if (m_playerTag == PlayerInput.playerTag.None)
        {
            Debug.LogWarning("PlayerTag set to no player. Add playerTag in prefab. Ex: Player1, Player2");
        }
        else
        {
            PlayerInput player = gm.getPlayerInput((int)m_playerTag);
            if (player.m_controller == null)
            {
                player.m_controller = this;
            }
            else
            {
                Debug.LogWarning("Replacing PlayerController that is already connected to PlayerInput");
            }
        }
    }
    public void Interact()
    {
        //check different things that can be interacted with and interact accordingly

        //if hand is empty, fill hand 
        if(/*m_heldItem == null*/ false)
        {

        }
        else //holding item
        {
            if(/*In range of item*/false)
            {
                //call pick up on item
            }

        }

        //in range of object and hands are empty?
        //pick up object
        
    }

    public void Aim(Vector2 aimVector)
    {
        //move arrow that denotes aiming

        //Rotates around this (the character's) transform
        //About the z axis
        //for arctan(x/y) degrees, which is the angle provided by the aimvector
        /*  m_aimArrowTransform.RotateAround(transform.position, Vector3.back, 
              Mathf.Atan(aimVector.y / aimVector.x)); 
              */
        float angle = Mathf.Atan2(aimVector.y,aimVector.x) * Mathf.Rad2Deg;
        Debug.Log("Angle: " + angle);
        m_aimArrowTransform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        m_aimArrowTransform.position = transform.position + Quaternion.AngleAxis(angle, Vector3.back) * new Vector3(m_arrowDistance, 0, 0);

    }

    public void Move(Vector2 velocity) //should be called in FixedUpdate
    {
        //check status effects to see if possible to move
        if(m_stunned)
        {
            velocity = Vector2.zero;
        }
        //if it is possible to move then move according to the input
        Debug.Log("New velocity for " + m_playerTag + " : " + (velocity * m_speedFactor));
        m_rigidbody.AddForce(velocity* m_speedFactor);
    }

    public void Stun(float stunTime) //other things call this to stun the character
    {
        m_stunTime = stunTime;
        m_stunned = true;
        StartCoroutine("StunCoroutine");
    }

    private IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(m_stunTime);
        m_stunTime = 0;
        m_stunned = false;
    }
}
