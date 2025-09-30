using UnityEngine;

public class AbilityFactory
{
    private AbilityFactory() 
    {
    
    }
    private static AbilityFactory instance;
    public static AbilityFactory GetInstance()
    {
        if (instance == null)
        {
            instance = new AbilityFactory();
        }
        
        return instance;
    }
    public bool GetAbility(AbilityType abilityType, out Ability ability)
    {
        ability = null;
        switch (abilityType)
        {
            case AbilityType.TakeCare:
                ability = new AB_TakeCare();
                break;
            case AbilityType.MagicTrick:
                ability = new AB_MagicTrick();
                break;
            case AbilityType.Feed:
                ability = new AB_Feed();
                break;
            case AbilityType.Play:
                ability = new AB_Play();
                break;
           
        }
        return ability != null;
    }
}
