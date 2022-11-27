using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("GameOver")]
    public GameObject gameOverPanel;
    public bool isGameOver;

    [Header("Pause")]
    public GameObject pausePanel;
    public bool isPaused;

    [Header("Player Status")]
    public int coins;

    [Header("HudPlayer Status")]
    public GameObject topPanel;
    public TextMeshProUGUI playerTxt;
    public Image healthBar;
    // Death Count
    public int mobsDeafeted;
    public TextMeshProUGUI deafetedCount;

    [Header("Other Controllers")]
    public Player playerController;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //Update the Hud
        healthBar.fillAmount = playerController.playerHealth / 10;
        deafetedCount.SetText("Killed: {}", mobsDeafeted);
    }


    public void gameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Pause()
    {
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            topPanel.SetActive(false);
        }
        else if (isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            topPanel.SetActive(true);
        }
    }
}
