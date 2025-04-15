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
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        SceneManager.LoadScene("Level" + currentLevel);
        if (currentLevel < 2)
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
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

        int clonesPerRow = 3;
        float spacing = 0.8f;
        Vector3 playerPos = mainPlayer.transform.position;

        int existingClones = soldierCount - amount - 1;
        int startRow = existingClones / clonesPerRow;

        for (int i = 0; i < amount; i++)
        {
            int cloneIndex = existingClones + i;
            int row = cloneIndex / clonesPerRow;
            int col = cloneIndex % clonesPerRow;

            float offsetX = -(row + 1) * spacing;
            float offsetZ = (col - 1) * spacing;
            Vector3 spawnPosition = playerPos + new Vector3(offsetX, 0, offsetZ);

            Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
            GameObject newSoldier = Instantiate(mainPlayer, spawnPosition, spawnRotation);
            newSoldier.name = "SoldierClone";
            newSoldier.tag = "SoldierClone";
            newSoldier.SetActive(true);

            AddScore(10);
        }
    }

    public void RemoveSoldiers(int amount)
    {
        if (amount <= 0) return;

        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("SoldierClone");
        int removed = 0;

        foreach (GameObject soldier in soldiers)
        {
            if (soldier.name == "SoldierClone" && removed < amount)
            {
                Destroy(soldier);
                removed++;
                Debug.Log($"Removed soldier: {soldier.name}, Total removed: {removed}");
            }
        }

        soldierCount -= removed;
        Debug.Log($"Total soldiers: {soldierCount}");
    }
}