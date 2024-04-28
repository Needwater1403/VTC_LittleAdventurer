using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterControlCombat : MonoBehaviour
{
    [HideInInspector] public Health health;
    [HideInInspector] public List<Collider> targetList;
    [Title("Colliders")]
    public List<DamageCollider> colliderList;
    [Title("Projectile")]
    [SerializeField] private Bullet projectilePrefabs;
    [SerializeField] private Transform spawnPos;

    [Title("Enemy")] public Constants.EnemyType type;
    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        targetList = new List<Collider>();
        SetCollider();
    }

    protected virtual void InflictDamage()
    {
        targetList.Clear();
        SetCollider(true);
        InitAttackSFX();
        if (gameObject.CompareTag(Constants.PlayerTag))
        {
            GetComponent<ControlMovement>().ResetAtkStartTime();
        }
    }
    protected virtual void ResetTargetList()
    {
        targetList.Clear();
        SetCollider();
    }

    protected virtual void ShootBullet()
    {
        InitAttackSFX();
        Instantiate(projectilePrefabs, spawnPos.position, transform.rotation);
    }

    private void SetCollider(bool status = false)
    {
        foreach (var dc in colliderList)
        {
            dc.GetComponent<Collider>().enabled = status;
        }
    }
    private void InitAttackSFX()
    {
        if (gameObject.CompareTag(Constants.PlayerTag))
        {
           GetComponent<AudioManager>().PlayAudio(Constants.Blade);
        }
        else
        {
            if (type == Constants.EnemyType.Range)
            {
                GetComponent<AudioManager>().PlayAudio(Constants.LaserGun);
            }
            else
            {
                GetComponent<AudioManager>().PlayAudio(Constants.Slam);
            }
        }
    }
}
