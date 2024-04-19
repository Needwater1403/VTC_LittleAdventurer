using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Title("List of Enemy & Position")]
    public List<SpawnPos> spawnList;
    [Title("Gate")]
    [SerializeField] private List<Gate> gateList = new List<Gate>();
    private bool canSpawn = true;
    private bool stageClear;
    private bool stageStart;
    private List<GameObject> enemyList = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag) && canSpawn)
        {   
            canSpawn = false;
            StartCoroutine(SpawnEnemy());
        }
    }

    private void Update()
    {
        if (!stageStart || stageClear) return;
        if (enemyList.Count == 0)
        {
            stageClear = true;
            foreach (var gate in  gateList)
            {
                gate.GateOpen();
            }
            Debug.Log("<color=Green>StageClear</color> ");
        }
        else
        {
            foreach (var enemy in enemyList.Where(enemy => enemy == null))
            {
                enemyList.Remove(enemy);
                break;
            }
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
                enemyList.Add(Instantiate(spawnPos.Enemy, transform1.position, transform1.rotation));
            }
        }

        stageStart = true;
        yield return null;
    }
}
