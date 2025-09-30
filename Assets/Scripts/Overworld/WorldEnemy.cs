
using UnityEngine;

public class WorldEnemy : MonoBehaviour
{
    public CombatEnemy enemy;


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            EnemyManager.Instance.currentEnemy = enemy;
            EnemyManager.Instance.StartCombat();
        }
    }
}