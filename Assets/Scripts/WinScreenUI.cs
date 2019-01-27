using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
    public GameObject winnerPanel;

    // Start is called before the first frame update
    void Start()
    {
        winnerPanel.SetActive(false);
    }

    public void DisplayWinner(string winner)
    {
        winnerPanel.SetActive(true);
        GameObject winnerText = GameObject.FindGameObjectWithTag("WinnerText");
        Text text = winnerText.GetComponent<Text>();
        text.text = winner + "Wins!";
    }

    public void Rematch()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
