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
    private void Awake()
    {
        DontDestroyOnLoad(transform.parent);
    }

    private void Start()
    {
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
        if (player != null || gameOver) return; 
        gameOver = true;
        Debug.Log("<color=red>YOU DIED!!!</color> ");
    }
}
