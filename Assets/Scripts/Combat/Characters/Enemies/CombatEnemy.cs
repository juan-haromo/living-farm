
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CombatEnemy : MonoBehaviour, ITargetable
{
    [SerializeField] float maxFriendship;
    float currentFriendship = 0;
    [SerializeField] float maxPatience;
    float currentPatience = 0;

    [SerializeField] Image friendshipBar;
    [SerializeField] Image patienceBar;
    [SerializeField] TextMeshProUGUI lblLvl;

    [SerializeField] Transform selectedPosition;
    public string EnemyName;
    float levelModifier;

    bool wasFriendshipAdded = false;

    void Start()
    {
        int level = EnemyManager.Instance.GetEnemyLevel(EnemyName);
        lblLvl.text = level.ToString();
        double num = Math.Log(1d + 0.1d * level);
        double den = Math.Log(1 + 0.1 * 100);
        levelModifier = (float)(num / den);
        currentPatience = maxPatience;
        UpdateBars();
        CombatManager.Instance.OnTurnEnd += OnTurnEnd;
    }

    void OnDisable()
    {
        CombatManager.Instance.OnTurnEnd -= OnTurnEnd;
    }

    public void AddFriendship(float amount)
    {
        amount *= 1 - levelModifier;
        currentFriendship = Mathf.Min(currentFriendship + Mathf.Abs(amount), maxFriendship);
        wasFriendshipAdded = true;
        if(currentFriendship >= maxFriendship){ Recruited(); }
        UpdateBars();
    }

    public void AddPatience(float amount)
    {
        amount *= (1 - levelModifier);
        currentPatience = Mathf.Min(currentPatience + Mathf.Abs(amount), maxPatience);
        UpdateBars();
    }

    public void RemovePatience(float amount)
    {
        amount *= (1 + levelModifier);
        currentPatience = Mathf.Max(currentPatience - Mathf.Abs(amount), 0);
        if (currentPatience <= 0){CombatManager.Instance.EndCombat(false);}
        UpdateBars();
    }

    void Recruited()
    {
        CombatManager.Instance.OnTurnEnd -= OnTurnEnd;
        CombatManager.Instance.RecruitEnemy(this);
        gameObject.SetActive(false);
    }

    public virtual void OnTurnEnd()
    {
        if (!wasFriendshipAdded)
        {
            RemovePatience(10);
        }
        wasFriendshipAdded = false;
    }

    void UpdateBars()
    {
        friendshipBar.fillAmount = currentFriendship / maxFriendship;
        patienceBar.fillAmount = currentPatience / maxFriendship;
    }

    public Transform GetTargetPosition() => selectedPosition;

    public CombatEnemy GetEnemy() => this;
    
    [SerializeField] private List<Attack> attacks;
    public void StartAttack()
    {
        int i = UnityEngine.Random.Range(0, attacks.Count);
        StartCoroutine(attacks[i].ActivateAttack(levelModifier));
    }
}