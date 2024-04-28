using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Title("Player")]
    [SerializeField] private PlayerManager player;
    public PlayerManager Player=> player;

    private bool gameOver;
    public bool GameOver
    {
        get => gameOver;
        set => gameOver = value;
    }

    private bool isPausedAfterComplete;

    private void Start()
    {
        Time.timeScale = 1;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isPausedAfterComplete) return;
        if (Player.CoinNum == 4)
        {
            Time.timeScale = 0;
            Player.Pause(true);
            UIManager.Instance.ShowWinPanel(true);
        }

        if (!gameOver) return;
        isPausedAfterComplete = true;
        UIManager.Instance.ShowLosePanel(true);
        Time.timeScale = 0;
    }
}
