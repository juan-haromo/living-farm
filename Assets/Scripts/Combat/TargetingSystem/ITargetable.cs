using UnityEngine;

public interface ITargetable
{
    public Transform GetTargetPosition();
    public CombatEnemy GetEnemy();
}