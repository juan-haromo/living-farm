using UnityEngine;

public class AB_MagicTrick : Ability
{
    public AB_MagicTrick()
    {
        Name = "Magic trick";
        Description = "Play a some tricky magic";
    }

    public override void Activate(CombatAlly attacker, CombatEnemy target)
    {
        target.AddFriendship(60);
    }
}
