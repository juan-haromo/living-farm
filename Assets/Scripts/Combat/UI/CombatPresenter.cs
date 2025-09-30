using System.Collections;
using UnityEngine;

public class CombatPresenter
{
    CombatView view;

    public CombatPresenter(CombatView view)
    {
        this.view = view;
    }

    public void SetModel(CombatAlly options)
    {
        view.btnOption1.onClick.RemoveAllListeners();
        view.btnOption2.onClick.RemoveAllListeners();
        view.btnOption3.onClick.RemoveAllListeners();
        view.btnOption4.onClick.RemoveAllListeners();

        view.lblOption1.text = options.Ability1.Name;
        view.lblOption2.text = options.Ability2.Name;
        view.lblOption3.text = options.Ability3.Name;
        view.lblOption4.text = options.Ability4.Name;

        view.btnOption1.onClick.AddListener(() =>
        {
            SelecTarget(options.Ability1.Activate, CombatManager.Instance.combatAlly);

        });
        view.btnOption2.onClick.AddListener(() =>
        {
            SelecTarget(options.Ability2.Activate, CombatManager.Instance.combatAlly);
        });
        view.btnOption3.onClick.AddListener(() =>
        {
            SelecTarget(options.Ability3.Activate, CombatManager.Instance.combatAlly);
        });
        view.btnOption4.onClick.AddListener(() =>
        {
            SelecTarget(options.Ability4.Activate, CombatManager.Instance.combatAlly);
        });
    }

    void SelecTarget(Command command, CombatAlly ally)
    {
        Debug.Log("selecting target");
        view.gameObject.SetActive(false);
        CombatManager.Instance.StartCoroutine(TargetAwait(command,ally));

    }

    IEnumerator TargetAwait(Command command, CombatAlly ally)
    {
        view.gameObject.SetActive(false);
        CombatManager.Instance.targetingSystem.StartSelection();
        yield return new WaitUntil(() => !CombatManager.Instance.targetingSystem.IsSelecting);
        if (CombatManager.Instance.targetingSystem.Target == null)
        {
            view.gameObject.SetActive(true);
            yield return null;
        }
        else
        {
            command(ally, CombatManager.Instance.targetingSystem.Target.GetEnemy());
            CombatManager.Instance.StartAttack();
            yield return null;
        }
    }

    delegate void Command(CombatAlly ally, CombatEnemy enemy); 
}
