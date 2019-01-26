using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int NumberOfPlayers = 4;

    [SerializeField]
    public PlayerInput[] playerInputs = new PlayerInput[NumberOfPlayers];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NumberOfPlayers; i++) //make player input classes for each controller
        {
            playerInputs[i] =  new PlayerInput(i + 1);
        }
    }

    public PlayerInput getPlayerInput(int playerNumber)
    {
        return playerInputs[playerNumber - 1];
    }

    public void allowCharacterMovement()
    {
        foreach (PlayerInput pi in playerInputs)
        {
            pi.allowCharacterMovement = true;
        }
    }

    public void allowCharacterMovement(int playerNumber) //overload to individually allow character movement
    {
        playerInputs[playerNumber - 1].allowCharacterMovement = false;
    }

    public void disallowCharacterMovement()
    {
        foreach (PlayerInput pi in playerInputs)
        {
            pi.allowCharacterMovement = true;
        }
    }

    public void disallowCharacterMovement(int playerNumber) //overload to individually disallow character movement
    {
        playerInputs[playerNumber - 1].allowCharacterMovement = false;
    }


// Update is called once per frame
void Update()
    {


    }
}
