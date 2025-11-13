using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PushButton : MonoBehaviour
{
    public Direction pushDir;
    public Button button;
    public Movable body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        body = GetComponentInParent<Movable>();
        Debug.Log(body);
        button.onClick.AddListener(() => body.Move(pushDir.ToVec()));
    }

}
