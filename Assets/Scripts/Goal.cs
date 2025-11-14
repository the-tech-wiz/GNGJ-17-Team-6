using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool taken = false;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            taken = true;
            Goal[] goals = transform.parent.GetComponentsInChildren<Goal>();
            bool isAll = true;
            foreach(Goal plate in goals){
                if (!plate.taken) isAll = false;
            }
            if(isAll)
                SceneController.instance.NextLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            taken = false;
        }
    }
}
