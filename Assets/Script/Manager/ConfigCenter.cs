using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigCenter : MonoBehaviour
{
    public static ConfigCenter Instance;
    [SerializeField] private ConfigMovementSO configMovement;
    [SerializeField] private ConfigCombatSO playerConfigCombat;
    [SerializeField] private ConfigCombatSO enemy1ConfigCombat;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public ConfigMovementSO GetPLayerConfigMovement()
    {
        return configMovement;
    }

    public ConfigCombatSO GetPlayerConfigCombat()
    {
        return playerConfigCombat;
    }

    public ConfigCombatSO GetEnemy1ConfigCombat()
    {
        return enemy1ConfigCombat;
    }
}