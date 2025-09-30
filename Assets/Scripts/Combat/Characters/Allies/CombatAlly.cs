using System;
using UnityEngine;
using UnityEngine.UI;

public class CombatAlly : MonoBehaviour
{
    public Ability Ability1 { get; private set; }
    public Ability Ability2 { get; private set; }
    public Ability Ability3 { get; private set; }
    public Ability Ability4 { get; private set; }

    [SerializeField] float maxStamina;
    float currentStamina;
    [SerializeField] Image staminaBar;

    public CombatEnemy target;

    public void SetUp(DataCombatAlly data)
    {
        name = data.CharacterName;
        Ability _ability;
        if (AbilityFactory.GetInstance().GetAbility(data.ability1, out _ability))
        {
            Ability1 = _ability;
        }
        else
        {
            Debug.LogError("Ability " + data.ability1 + " does not exist");
        }

        if (AbilityFactory.GetInstance().GetAbility(data.ability2, out _ability))
        {
            Ability2 = _ability;
        }
        else
        {
            Debug.LogError("Ability " + data.ability2 + " does not exist");
        }


        if (AbilityFactory.GetInstance().GetAbility(data.ability3, out _ability))
        {
            Ability3 = _ability;
        }
        else
        {
            Debug.LogError("Ability " + data.ability3 + " does not exist");
        }


        if (AbilityFactory.GetInstance().GetAbility(data.ability4, out _ability))
        {
            Ability4 = _ability;
        }
        else
        {
            Debug.LogError("Ability " + data.ability4 + " does not exist");
        }

        currentStamina = maxStamina;

        CombatManager.Instance.OnTurnEnd += OnTurnEnd;
    }

    void OnDisable()
    {
        CombatManager.Instance.OnTurnEnd -= OnTurnEnd;
    }

    bool wasStaminaAdded = false;

    void OnTurnEnd()
    {
        if (!wasStaminaAdded)
        {
            RemoveStamina(10);
        }
        wasStaminaAdded = false;
    }

    public void AddStamina(float amount)
    {
        currentStamina = MathF.Min(currentStamina + Mathf.Abs(amount), maxStamina);
        wasStaminaAdded = true;
        UpdateStamina();
    }

    public void RemoveStamina(float amount)
    {
        currentStamina = MathF.Max(currentStamina - Mathf.Abs(amount), 0);
        if(currentStamina<=0){ CombatManager.Instance.EndCombat(false); }
        UpdateStamina();
    }

    void UpdateStamina()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void Damage(float amount)
    {
        RemoveStamina(amount); 
    }
}