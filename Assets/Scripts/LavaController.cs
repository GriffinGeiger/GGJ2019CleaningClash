using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public GameObject[] players = new GameObject[4];
    public GameObject bed;
    public GameObject desk;
    private double timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 10;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            if(players[i] != null)
            {
                Collider2D[] nearbyInteractables = 
                    Physics2D.OverlapAreaAll((Vector2)players[i].transform.position + players[i].m_topCornerOfOverlap, (Vector2)players[i].transform.position - m_bottomCornerOfOverlap);
            }
        }
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(this);
        }
    }
}
