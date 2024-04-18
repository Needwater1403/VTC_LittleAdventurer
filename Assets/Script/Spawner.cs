using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<SpawnPos> spawnList;
    private bool canSpawn = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag) && canSpawn)
        {   
            canSpawn = false;
            StartCoroutine(SpawnEnemy());
        }
    }

    
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        foreach (var spawnPos in spawnList)
        {
            if (spawnPos != null)
            {
                var transform1 = spawnPos.transform;
                Instantiate(spawnPos.Enemy, transform1.position, transform1.rotation);
            }
        }

        yield return null;
    }
}
