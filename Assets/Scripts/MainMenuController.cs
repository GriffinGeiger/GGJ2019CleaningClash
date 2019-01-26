using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject creditsPanel;
    public GameObject startBtn;

    // Start is called before the first frame update
    void Start()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void DisplayInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void DisplayCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(startBtn);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
