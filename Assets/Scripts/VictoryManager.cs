using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryScreenUI;
    public GameObject failScreenUI;

    public void Win(){
        victoryScreenUI.SetActive(true);
    }
    
    public void Fail(){
        failScreenUI.SetActive(true);
    }

    public void NextLevel(){
        SceneController.instance.NextLevel();
    }

    public void Home(){
        SceneController.instance.LoadScene("Main Menu");
    }

    public void Retry(){
        SceneController.instance.Reload();
    }
}
