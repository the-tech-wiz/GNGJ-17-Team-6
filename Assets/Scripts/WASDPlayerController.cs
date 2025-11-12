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
        if (MoveLeft.triggered) body.Move(Vector3.left, 1);
        else if (MoveRight.triggered) body.Move(Vector3.right, 1);
        else if (MoveUp.triggered) body.Move(Vector3.up, 1);
        else if (MoveDown.triggered) body.Move(Vector3.down, 1);

        /*         if (MoveLeft.triggered) body.MoveUntilStopped(Vector3.left);
                else if (MoveRight.triggered) body.MoveUntilStopped(Vector3.right);
                else if (MoveUp.triggered) body.MoveUntilStopped(Vector3.up);
                else if (MoveDown.triggered) body.MoveUntilStopped(Vector3.down); */
    }
}
