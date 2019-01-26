using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int NumberOfPlayers = 4;
    private PlayerInput[] playerInputs = new PlayerInput[NumberOfPlayers];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < NumberOfPlayers; i++) //make player input classes for each controller
        {
            playerInputs[i] = new PlayerInput(i+1);
        }

    }

    public PlayerInput getPlayerInput(int playerNumber)
    {
        return playerInputs[playerNumber - 1];
    }

    // Update is called once per frame
    void Update()
    {


    }
}
