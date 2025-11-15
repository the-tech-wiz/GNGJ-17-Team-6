using UnityEngine;

public class Goal : MonoBehaviour
{
    void Start(){
        VictoryManager[] winScreens = transform.parent.parent.GetComponentsInChildren<VictoryManager>();
        winScreen = winScreens[0];
        edge = this.GetComponent<BoxCollider2D>();
    }

    public VictoryManager winScreen;
    public BoxCollider2D edge;
    public int taken = 0;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            //AudioManager.instance.Play("Plate");
            taken += 1;
            //Goal[] goals = transform.parent.GetComponentsInChildren<Goal>();
            bool isAll = true;
            foreach(Goal plate in transform.parent.GetComponentsInChildren<Goal>()){
                if (plate.taken == 0) isAll = false;
            }
            if(isAll)
                if(Stopped())
                    winScreen.Win();
        }
    }

    private bool Stopped(){
        Transform cat = GameObject.Find("Controllables").GetComponent<Transform>();
        if(cat == null){
            Debug.LogWarning("Controllables not found!");
            return true;
        }
        foreach(Movable slime in cat.GetComponentsInChildren<Movable>()){
            if(slime.origin)
                return false;
        }
        return true;
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
