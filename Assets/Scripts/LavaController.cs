using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public CharacterMovement[] players;
    public GameObject bed;
    public GameObject desk;
    private double timeLeft;
    bool burned = false;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 10;
        players =  FindObjectsOfType<CharacterMovement>();
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
                    burned = true;
                }
                
            }
            
        }
        if (burned)
        {
            Destroy(this.gameObject);
        }
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
