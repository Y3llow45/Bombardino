using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int soldierCount = 1;
    public GameObject mainPlayer;
    public TMP_Text scoreText;
    public TMP_Text scoreTextFinish;
    public GameObject gameOverUI;
    public GameObject CanvasFinish;
    public PauseMenu pauseMenu;
    private int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreTextFinish.text = "Score: " + score;
            scoreText.text = "Score: " + score;
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        CanvasFinish.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void SpawnSoldiers(int amount)
    {
        if (mainPlayer == null)
        {
            Debug.LogError("Main Player is missing! Cannot spawn soldiers.");
            return;
        }

        soldierCount += amount;
        Debug.Log($"Total soldiers: {soldierCount}");

        int columns = 4;
        float spacing = 1.5f;

        for (int i = 0; i < amount; i++)
        {
            AddScore(10);

            int row = i / columns;
            int col = i % columns;

            float offsetX = -row * spacing;
            float offsetZ = (col - 1.5f) * spacing;

            Vector3 spawnPosition = mainPlayer.transform.position + new Vector3(offsetX, 0, offsetZ);

            GameObject newSoldier = Instantiate(mainPlayer, spawnPosition, mainPlayer.transform.rotation);
            newSoldier.name = "SoldierClone";
            PlayerController follower = newSoldier.GetComponent<PlayerController>();
            newSoldier.SetActive(true);
        }
    }

    public void RemoveSoldiers(int amount)
    {
        soldierCount -= amount;
        if (soldierCount < 1)
        {
            GameOver();
        }

        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Player");
        int removed = 0;

        foreach (GameObject soldier in soldiers)
        {
            if (soldier.name == "SoldierClone" && removed < amount)
            {
                Destroy(soldier);
                removed++;
            }
        }
    }
}