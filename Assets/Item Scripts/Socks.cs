using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socks : Throwable
{
    bool stink = false;
    public float stenchRadius = 4f;
    public float stenchForce = 100f;

    void Update()
    {
        if (Thrown && !InAir)
        {
            stink = true;
            StartCoroutine("StenchCloud");
        }
        else
            stink = false;
    }

    IEnumerator StenchCloud()
    {
        //Repel player if nearby
        LayerMask playerMask = LayerMask.GetMask("Players");
        Collider2D player = Physics2D.OverlapCircle(transform.position, stenchRadius, playerMask);
        if(player != null) //player is in my swamp
        {
            Vector3 pushVector = (transform.position - player.gameObject.transform.position).normalized;
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(pushVector * stenchForce); 
        }
            
        Thrown = false;
        yield return new WaitForSeconds(5.0f);
    }
}
