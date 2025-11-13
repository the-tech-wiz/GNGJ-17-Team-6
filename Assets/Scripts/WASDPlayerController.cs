using UnityEngine;
using UnityEngine.InputSystem;

public class WASDPlayerController : MonoBehaviour
{
    public bool head;
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
        if (head)
        {
            body.origin = true;
            if (MoveLeft.triggered) body.GetAhead(Vector2.left);
            if (MoveRight.triggered) body.GetAhead(Vector2.right);
            if (MoveUp.triggered) body.GetAhead(Vector2.up);
            if (MoveDown.triggered) body.GetAhead(Vector2.down);

            /*             if (MoveLeft.triggered) body.MoveUntilStopped(Vector3.left);
                        else if (MoveRight.triggered) body.MoveUntilStopped(Vector3.right);
                        else if (MoveUp.triggered) body.MoveUntilStopped(Vector3.up);
                        else if (MoveDown.triggered) body.MoveUntilStopped(Vector3.down) */
            ;
        }
    }
}
