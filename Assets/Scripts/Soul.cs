using UnityEngine;

public class Soul : MonoBehaviour
{

    Vector2 movement;

    public CombatAlly ally;
    [SerializeField] float speed;
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        if(InputManager.Instance == null){ return; }
        InputManager.Instance.Input.Movement.Enable();
    }

    void OnDisable()
    {
        if(InputManager.Instance == null){ return; }
        InputManager.Instance.Input.Movement.Disable();
    }

    void Update()
    {
        movement = InputManager.Instance.Input.Movement.Move.ReadValue<Vector2>();
        transform.Translate(Time.deltaTime * speed * movement.normalized);
    }
}
