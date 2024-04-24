using UnityEngine;

[CreateAssetMenu(fileName = "ConfigMovementSO", menuName = "Config/Config Movement")]
public class ConfigMovementSO : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public float rotationSpeed;
    public float gravity;
    public float slideDuration;
    public float slideSpeed;
    public float attackCooldown;
    public float rollSpeed;
    public float rollCooldown;
}
