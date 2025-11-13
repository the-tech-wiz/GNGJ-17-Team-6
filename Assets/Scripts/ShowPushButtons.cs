using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowPushButtons : MonoBehaviour
{
    Movable body;
    public PushButton[] buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponentInParent<Movable>();
        buttons = GetComponentsInChildren<PushButton>();
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var neighs = body.Neighbours();
        Debug.Log(String.Join(", ", neighs));
        foreach (var button in buttons)
        {

            if (neighs.Contains(button.pushDir.Opposite()))
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.gameObject.SetActive(true);
            }
        }


    }
}
