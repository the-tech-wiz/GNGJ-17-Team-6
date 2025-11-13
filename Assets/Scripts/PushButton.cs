using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PushButton : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public Direction pushDir;
    public Button button;
    public Movable body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        body = GetComponentInParent<Movable>();
        Debug.Log(body);
        button.onClick.AddListener(() => body.Move(ToVec(pushDir)));
    }
    Vector2 ToVec(Direction dir) =>
        dir switch
        {
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            _ => Vector2.zero
        };


}
