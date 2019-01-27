using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public CharacterMovement[] players;
    public GameObject bed;
    public GameObject desk;
    private double timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 10;
        players = new CharacterMovement[2];
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 2; i++)
        {
            if(players[i] != null)
            {
                if(!players[i].onFurniture)
                {
                    players[i].Stun(2.0f);
                    Destroy(this.gameObject);
                }
                
            }
            
        }
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
