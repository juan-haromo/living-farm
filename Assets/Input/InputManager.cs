using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public PlayerInput Input {  get;  set; }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            Input = new PlayerInput();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Destroying duplicated input manager " + name);
            Destroy(this);
        }
    }

    public void EnableUI()
    {
        DisableAll();
        Instance.Input.UI.Enable();
    }

    public void EnableMovement()
    {
        DisableAll();
        Instance.Input.Movement.Enable();
    }

    void DisableAll()
    {
        Instance.Input.Disable();
    }
}
