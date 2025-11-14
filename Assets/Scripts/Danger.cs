using UnityEngine;

public class Danger : MonoBehaviour
{
    public VictoryManager failScreen;

    void Start()
    {
        failScreen = GameObject.Find("Menus").GetComponent<VictoryManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            failScreen.Fail();
        }
    }
}
