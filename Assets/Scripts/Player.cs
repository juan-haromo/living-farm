using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput input;
    Vector2 movement;
    CharacterController controller;

    [SerializeField] float speed = 5.0f;
    

    private void Start()
    {
        if (TryGetComponent<CharacterController>(out CharacterController controller))
        {
            this.controller = controller;
            this.controller.detectCollisions = true;
        }
        else
        {
            Debug.LogError("Character does not have a controller");
        }

        input = new PlayerInput();
        input.Movement.Enable();
    }

    private void OnDestroy()
    {
        input.Movement.Disable();
    }

    private void Update()
    {
        movement = input.Movement.Move.ReadValue<Vector2>();

        controller.Move(Time.deltaTime * speed * movement.normalized);
    }
}
