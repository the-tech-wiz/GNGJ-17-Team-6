using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryScreenUI;
    public GameObject failScreenUI;

    public void Win(){
        AudioManager.instance.Play("Win");
        victoryScreenUI.SetActive(true);
    }
    
    public void Fail(){
        AudioManager.instance.Play("Lose");
        failScreenUI.SetActive(true);
    }

    public void NextLevel(){
        SceneController.instance.NextLevel();
    }

    public void Home(){
        SceneController.instance.LoadScene("Level Select");
    }

    public void Retry(){
        SceneController.instance.Reload();
    }
}
