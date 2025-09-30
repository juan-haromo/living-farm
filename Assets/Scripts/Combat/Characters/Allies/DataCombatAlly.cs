using UnityEngine;

[CreateAssetMenu(fileName = "DataCombatAlly", menuName = "Characters/DataCombatAlly")]
public class DataCombatAlly : ScriptableObject
{
    public string CharacterName;
    public AbilityType ability1;
    public AbilityType ability2;
    public AbilityType ability3;
    public AbilityType ability4;
}
