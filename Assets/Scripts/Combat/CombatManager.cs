using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public delegate void TurnEnd();
    public event TurnEnd OnTurnEnd;

    CombatPresenter combatPresenter;
    [SerializeField] CombatView view;
    [SerializeField] DataCombatAlly dataCombatAlly;
    [SerializeField] Transform allyPrefab;
    [SerializeField] Transform enemyPrefab;
    public CombatAlly combatAlly { get; private set; }

    public TargetingSystem targetingSystem;

    public Soul soul;
    public GameObject combatZone;
    CombatEnemy enemy;

    [SerializeField] Scene CombatScene;

    void Start()
    {
        defeatedEnemies = new System.Collections.Generic.List<string>();
        StartCombat();
    }

    public void StartCombat()
    {
        defeatedEnemies.Clear();
        enemy = EnemyManager.Instance.currentEnemy;
        combatZone.SetActive(false);
        soul.gameObject.SetActive(false);
        if (view == null) { Debug.LogError("No view was assigned to " + name); return; }
        combatPresenter = new CombatPresenter(view);

        combatAlly = Instantiate(allyPrefab, new Vector3(-5, 0, 0), Quaternion.identity).GetComponent<CombatAlly>();
        combatAlly.SetUp(dataCombatAlly);

        for (int i = 0; i < 3; i++)
        {
            CombatEnemy combatEnemy = Instantiate(enemy.gameObject, new Vector3(10, -5 + (i * 5), 0), Quaternion.identity).GetComponent<CombatEnemy>();

            combatAlly.target = combatEnemy;

            Debug.Log(combatEnemy.name);

            targetingSystem.enemyTargets.Add(combatEnemy);
        }

        combatPresenter.SetModel(combatAlly);
        soul.ally = combatAlly;
    }

    public void EndTurn()
    {
        OnTurnEnd?.Invoke();
        soul.gameObject.SetActive(false);
        combatZone.SetActive(false);
        view.gameObject.SetActive(true);
    }

    int currentAttacks = 0;
    public void StartAttack()
    {
        if (targetingSystem.enemyTargets.Count <= 0)
        {
            EndCombat(true);
            return;
        }
        soul.gameObject.SetActive(true);
        combatZone.SetActive(true);

        int totalAttack = 6 - targetingSystem.enemyTargets.Count;
        while (0 < totalAttack)
        {
            for (int i = 0; i < targetingSystem.enemyTargets.Count; i++)
            {
                currentAttacks++;
                totalAttack--;
                targetingSystem.enemyTargets[i].GetEnemy().StartAttack();
            }
        }
    }
    public void AttackDone()
    {
        currentAttacks--;
        if (currentAttacks == 0) { EndTurn(); }
    }
    
    System.Collections.Generic.List<string> defeatedEnemies;

    public void RecruitEnemy(ITargetable enemyTarget)
    {
        defeatedEnemies.Add(enemyTarget.GetEnemy().EnemyName);
        targetingSystem.enemyTargets.Remove(enemyTarget);

        if (targetingSystem.enemyTargets.Count == 0)
        {
            EndCombat(true);
        }
    }

    public void EndCombat(bool hasWon)
    {
        if (hasWon)
        {
            foreach (string e in defeatedEnemies)
            {
                EnemyManager.Instance.EnemyDefeated(e);
            }
        }
        SceneManager.LoadScene("PlayerTest");
    }

}

