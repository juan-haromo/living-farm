
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{

    public List<ITargetable> enemyTargets;
    public ITargetable Target{ get; private set; }
    int currentTarget = 0;
    [SerializeField] Transform selectPrefab;
    public bool IsSelecting { get; private set; }
    void Awake()
    {
        enemyTargets = new List<ITargetable>();
        selectPrefab.gameObject.SetActive(false);
        IsSelecting = false;
    }
    void OnDisable()
    {
        //StopSelection();
    }

    public void StartSelection()
    {
        Target = null;
        currentTarget = 0; 
        InputManager.Instance.Input.UI.Next.performed += NextTarget;
        InputManager.Instance.Input.UI.Previous.performed += PreviousTarget;
        InputManager.Instance.Input.UI.Submit.performed += CompleSelection;
        InputManager.Instance.Input.UI.Cancel.performed += CancelSelection;
        InputManager.Instance.Input.UI.Enable();
        selectPrefab.gameObject?.SetActive(true);
        UpdatePosition();
        IsSelecting = true;
    }

    private void CompleSelection(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Target = enemyTargets[currentTarget];
        StopSelection();
    }

    private void CancelSelection(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Target = null;
        StopSelection();
    }

    void StopSelection()
    {
        selectPrefab.gameObject?.SetActive(false);
        InputManager.Instance.Input.UI.Next.performed -= NextTarget;
        InputManager.Instance.Input.UI.Previous.performed -= PreviousTarget;
        InputManager.Instance.Input.UI.Cancel.performed -= CancelSelection;
        InputManager.Instance.Input.UI.Submit.performed -= CompleSelection;
        IsSelecting = false;
    }


    void NextTarget()
    {
        currentTarget = (currentTarget + 1) % enemyTargets.Count;
        UpdatePosition();
    }
    private void NextTarget(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        NextTarget();
    }


    void PreviousTarget()
    {
        currentTarget = (currentTarget - 1 + enemyTargets.Count) % enemyTargets.Count;
        UpdatePosition();
    }
    private void PreviousTarget(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        PreviousTarget();
    }


    void UpdatePosition()
    {
        selectPrefab.position = enemyTargets[currentTarget].GetTargetPosition().position;
    }
}
