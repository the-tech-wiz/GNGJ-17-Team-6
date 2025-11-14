using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryScreenUI;

    public void Win(){
        victoryScreenUI.SetActive(true);
    }

    public void NextLevel(){
        SceneController.instance.NextLevel();
    }

    public void Home(){
        SceneController.instance.LoadScene("Main Menu");
    }
}
