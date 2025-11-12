using UnityEngine;
using UnityEngine.InputSystem;

public class WASDPlayerController : MonoBehaviour
{
    public InputAction MoveUp;
    public InputAction MoveDown;
    public InputAction MoveLeft;
    public InputAction MoveRight;
    public Movable body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveUp.Enable();
        MoveDown.Enable();
        MoveLeft.Enable();
        MoveRight.Enable();
        body = GetComponent<Movable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveLeft.triggered) body.Move(new Vector2(-1, 0), 1);
        else if (MoveRight.triggered) body.Move(new Vector2(1, 0), 1);
        else if (MoveUp.triggered) body.Move(new Vector2(0, 1), 1);
        else if (MoveDown.triggered) body.Move(new Vector2(0, -1), 1);
    }
}
