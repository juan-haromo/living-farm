
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public CombatEnemy currentEnemy;
    [SerializeField] string combatScene;

    Dictionary<string, int> enemyLevel;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            enemyLevel = new Dictionary<string, int>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartCombat()
    {
        SceneManager.LoadScene(combatScene);
    }

    public int GetEnemyLevel(string enemyName)
    {
        return enemyLevel.ContainsKey(enemyName) ? enemyLevel[enemyName] : 1;
    }

    public void EnemyDefeated(string enemyName)
    {
        if (enemyLevel.ContainsKey(enemyName))
        {
            enemyLevel[enemyName]++;
        }
        else
        {
            enemyLevel.Add(enemyName, 1);
        }
    }

}
