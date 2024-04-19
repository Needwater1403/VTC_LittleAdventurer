using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager Instance;
    [Title("Player UI")] 
    [SerializeField] private Slider healthSlider;
    private float lastHealth;
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
        if (GameManager.Instance.Player.GetCurrentHP() - lastHealth == 0) return;
        lastHealth = GameManager.Instance.Player.GetCurrentHP();
        UpdateHealthUI(GameManager.Instance.Player.GetMaxHP());
    }

    private void UpdateHealthUI(float maxHP)
    {
        var percentage = lastHealth / maxHP ;
        healthSlider.value = (percentage);
    }
}
