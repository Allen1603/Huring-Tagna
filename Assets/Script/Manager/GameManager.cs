using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool hasWon = false;

    public GameObject winPanel;


    private void Start()
    {
        winPanel.SetActive(false);
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!activeEnemies.Contains(enemy))
            activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (!hasWon && activeEnemies.Count == 0)
        {
            hasWon = true;
            Debug.Log("YOU WIN!");
            // Optionally, call a method here:
            ShowWinScreen();
        }
    }

    private void ShowWinScreen()
    {
        //Time.timeScale = 0f;
        winPanel.SetActive(true);
    }
}
