using UnityEngine;

public class Goal : MonoBehaviour
{
    public VictoryManager winScreen;
    public BoxCollider2D edge;
    public int taken = 0;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            taken += 1;
            Goal[] goals = transform.parent.GetComponentsInChildren<Goal>();
            bool isAll = true;
            foreach(Goal plate in goals){
                if (plate.taken == 0) isAll = false;
            }
            if(isAll)
                winScreen.Win();
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            taken -= 1;
        }
    }

    /*void Update(){
        bool hasCat = false;
        Collider2D[] points = {};
        edge.GetContacts(points);
        foreach(Collider2D collision in points){
            if(collision.GetComponent<Collider>().CompareTag("Player")){
                hasCat = true;
                taken = true;
                Goal[] goals = transform.parent.GetComponentsInChildren<Goal>();
                bool isAll = true;
                foreach(Goal plate in goals){
                    if (!plate.taken) isAll = false;
                }
                if(isAll)
                    winScreen.Win();
            }
        }
        if(!hasCat) taken = false;
    }*/
}
