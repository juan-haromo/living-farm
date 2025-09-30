using UnityEngine;

public abstract class Ability  
{
    public Ability() { }
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public abstract void Activate(CombatAlly attacker, CombatEnemy target);
}
