using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//written by Griffin Geiger 

public class GameManager : MonoBehaviour
{
    public const int NumberOfPlayers = 2;
    public Vector3 player1SpawnLocation;
    public Vector3 player2SpawnLocation;
    public int numberOfItemsToSpawn = 10;

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
     /*   foreach( CharacterMovement cm in FindObjectsOfType<CharacterMovement>())
        {
            cm.ConnectToPlayerInput(); //send references from characterMovements to PlayerInputs. Ex: Player1 character tells Player1's controller who it is
        }
        */
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

    SetupPlayer player1Setup = new SetupPlayer(PlayerInput.playerTag.Player1);
    SetupPlayer player2Setup = new SetupPlayer(PlayerInput.playerTag.Player2);

    public GameObject player1Text;
    public GameObject player2Text;

    public GameObject momIntroAnimation;
    private bool introTimerStarted = false;
    private bool itemsSpawned = false;
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

                if (m_player1Ready)
                {
                    Debug.Log("Player1 Ready");
                    player1Text.SetActive(true);
                }
                if (m_player2Ready)
                    player2Text.SetActive(true);

                if(m_player1Ready && m_player2Ready)
                {
                    gameState = MatchState.Mom_Intro;
                }
                break;
            case MatchState.Mom_Intro:

                Debug.Log("In Mom_Intro");
                player1Text.SetActive(false);
                player2Text.SetActive(false);
                momIntroAnimation.SetActive(true);
                if (!m_playersSpawned) //check if players are spawned and if not, spawn them
                    m_playersSpawned = SpawnCharacters();

                if (!introTimerStarted)
                {
                    Debug.Log("Starting timer");
                    StartCoroutine("IntroTimer");
                    introTimerStarted = true;
                }
                    break;
            case MatchState.Gameplay:
                ////////////////////Gameplay////////////////////
                momIntroAnimation.SetActive(false);
                Debug.Log("In Gameplay state");
                if(!itemsSpawned)
                {
                    //spawn items
                    EventManager.SpawnItems(numberOfItemsToSpawn);
                    itemsSpawned = true;
                }







                //////////////////////////////////////////////////
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

    public IEnumerator IntroTimer()
    {
        Debug.Log("In introtimer");
        yield return new WaitForSeconds(8f);
        Debug.Log("timer up");
        gameState = MatchState.Gameplay;
    }

    private bool charactersSpawned;
    private CharacterMovement player1;
    private CharacterMovement player2;
    private bool SpawnCharacters() //They will be spawned but not allowed to move or connected to PlayerInputs. returns true if complete
    {
        if (!charactersSpawned)
        {
            player1 = InstantPrefabs.InstantiatePrefab(InstantPrefabs.player1Path, player1SpawnLocation).GetComponent<CharacterMovement>();
            player2 = InstantPrefabs.InstantiatePrefab(InstantPrefabs.player2Path, player2SpawnLocation).GetComponent<CharacterMovement>();
            charactersSpawned = true;
        }

        try
        {
            player1.ConnectToPlayerInput(this);
            player2.ConnectToPlayerInput(this);
        } catch(Exception) { return false; }
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
