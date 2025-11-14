using UnityEngine;

public class Danger : MonoBehaviour
{
    public VictoryManager failScreen;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            failScreen.Fail();
        }
    }
}
