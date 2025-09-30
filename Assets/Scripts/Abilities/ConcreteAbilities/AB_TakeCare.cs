using UnityEngine;

public class AB_TakeCare : Ability
{
    public AB_TakeCare()
    {
        Name = "Take care";
        Description = "Show some loving, show a lot of caring";
    }

    public override void Activate(CombatAlly attacker, CombatEnemy target)
    {
        target.AddFriendship(40);
        target.AddPatience(20);
        attacker.AddStamina(10);
    }
}
