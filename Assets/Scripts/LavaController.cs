using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public CharacterMovement[] players = new CharacterMovement[4];
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
                if(!players[i].onBed)
                {
                    players[i].Stun(3.0f);
                    Destroy(this);
                }
            }
        }
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(this);
        }
    }
}
