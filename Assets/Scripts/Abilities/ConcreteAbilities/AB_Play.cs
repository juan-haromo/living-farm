using UnityEngine;

public class AB_Play : Ability
{
    public AB_Play()
    {
        Name = "Play";
        Description = "Go for the play, go for the fun";
    }

    public override void Activate(CombatAlly attacker, CombatEnemy target)
    {
        target.AddFriendship(100);
        attacker.RemoveStamina(20);
    }
}
