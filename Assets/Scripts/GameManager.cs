using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Griffin Geiger 

public class GameManager : MonoBehaviour
{
    public const int NumberOfPlayers = 2;
    public Vector3 player1SpawnLocation;
    public Vector3 player2SpawnLocation;
    
    [SerializeField]
    public PlayerInput[] playerInputs = new PlayerInput[NumberOfPlayers];


    void Awake()
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
        foreach( CharacterMovement cm in FindObjectsOfType<CharacterMovement>())
        {
            cm.ConnectToPlayerInput(); //send references from characterMovements to PlayerInputs. Ex: Player1 character tells Player1's controller who it is
        }

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

    enum MatchState { Setup, Mom_Intro, Gameplay, Mom_Outro, Scoring}
    MatchState gameState = MatchState.Setup;
    // Update is called once per frame

    private bool m_playersSpawned;
    private bool m_player1Ready;
    private bool m_player2Ready;

    SetupPlayer player1Setup = new SetupPlayer();
    SetupPlayer player2Setup = new SetupPlayer();

void Update()
    {
        foreach (PlayerInput pi in playerInputs)
            pi.Update();

        switch (gameState)
        {
            case MatchState.Setup:
                //player1 setup sequence
                
                if(!m_player1Ready) //keep doing this until first true
                    m_player1Ready = player1Setup.PlayerSetup(player1SpawnLocation);
                //player2 setup sequence
                if(!m_player2Ready) //keep doing this until first true
                    m_player2Ready = player2Setup.PlayerSetup(player2SpawnLocation);

                if(m_player1Ready && m_player2Ready)
                {
                    gameState = MatchState.Mom_Intro;
                }
                break;
            case MatchState.Mom_Intro:

                if (!m_playersSpawned) //check if players are spawned and if not, spawn them
                    m_playersSpawned = SpawnCharacters();

                    break;
            case MatchState.Gameplay:
                break;
            case MatchState.Mom_Outro:
                break;
            case MatchState.Scoring:
                int winner = TallyScores();
                if (winner == 1)
                    Debug.Log("Player 1 wins!"); //do winner things here
                else if (winner == 2)
                {
                    Debug.Log("Player 2 wins!");
                }
                else if (winner == 0)
                {
                    Debug.Log("Everyone is grounded");
                }
                break;
            default:
                break;
        }
    }

   

    private bool SpawnCharacters() //They will be spawned but not allowed to move or connected to PlayerInputs. returns true if complete
    {
        try
        {
            InstantPrefabs.InstantiatePrefab(InstantPrefabs.player1Path, player1SpawnLocation);
            InstantPrefabs.InstantiatePrefab(InstantPrefabs.player2Path, player2SpawnLocation);
        } catch(Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
    }

    private void FixedUpdate()
    {
        foreach (PlayerInput pi in playerInputs)
            pi.FixedUpdate();
    }

    public int TallyScores() //returns 0 if tie, 1 if p1 wins, 2 if p2 wins
    {
        Item[] mess = FindObjectsOfType<Item>();
        int Player1Mess = 0;
        int Player2Mess = 0;

        foreach(Item junk in mess)
        {
            if(junk.transform.position.x > 0f)
            {
                Player2Mess++;
            }
            else if (junk.transform.position.x < 0f)
            {
                Player1Mess++;
            }
            else
            {
                Player1Mess++;  //Nearly impossible but if exactly on zero then it counts against both. Sucks to suck
                Player2Mess++;
            }
        }

        Debug.Log("Player 1's filth: " + Player1Mess + "\nPlayer 2's hoard: " + Player2Mess);
        if (Player1Mess < Player2Mess)
        {
            return 1;
        }
        else
        if (Player1Mess > Player2Mess)
        {
            return 2;
        }
        else
            return 0;

    }
}
