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

        List<Vector3> spawnPositions = new List<Vector3>();

        for (int i = 0; i < amount; i++)
        {
            AddScore(10);
            float offsetX = (i % 2 == 0 ? 1.5f : -1.5f) * ((i / 2) + 1);
            float offsetZ = (i / 2) * 1.5f;
            Vector3 spawnPosition = mainPlayer.transform.position + new Vector3(offsetX, 0, offsetZ);

            spawnPositions.Add(spawnPosition);
        }

        foreach (Vector3 pos in spawnPositions)
        {
            //GameObject newSoldier = Instantiate(mainPlayer, pos, Quaternion.identity);
            GameObject newSoldier = Instantiate(mainPlayer, pos, mainPlayer.transform.rotation);
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