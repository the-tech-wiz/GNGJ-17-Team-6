using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            transform.parent.GetComponent<Lock>().Unlock();
            gameObject.SetActive(false);
        }
    }
}
