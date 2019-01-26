using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D m_rigidbody;
    private float m_stunTime = 0;
    private bool m_stunned = false;
    //public Item m_heldItem;
    private Vector2 m_aimVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    }

    public void Move(Vector2 velocity) //should be called in FixedUpdate
    {
        //check status effects to see if possible to move
        if(m_stunned)
        {
            velocity = Vector2.zero;
        }
        //if it is possible to move then move according to the input
        m_rigidbody.velocity = velocity;
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
