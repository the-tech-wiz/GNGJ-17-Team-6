using UnityEngine;

public class Lock : MonoBehaviour
{
    private bool unlocked = false;
    public void Unlock(){
        unlocked = true;
    }

    public bool isLocked(){
        return !unlocked;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player") && unlocked){
            gameObject.SetActive(false);
        }
    }
}
