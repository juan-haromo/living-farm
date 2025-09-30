using UnityEngine;

public class AB_Feed : Ability
{
    public AB_Feed()
    {
        Name = "Feed";
        Description = "Feed w/ some delicious food";
    }

    public override void Activate(CombatAlly attacker, CombatEnemy target)
    {
        target.AddFriendship(40);
        target.AddPatience(10);
        attacker.AddStamina(20);
    }
}
