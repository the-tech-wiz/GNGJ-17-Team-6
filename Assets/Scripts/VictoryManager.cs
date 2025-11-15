using UnityEngine;
using System.Collections;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryScreenUI;
    public GameObject failScreenUI;

    public void Win(){
        int stepSound = UnityEngine.Random.Range(0, 2);
        if (stepSound == 0)
            AudioManager.instance.Play("Win1");
        else
            AudioManager.instance.Play("Win2");
        victoryScreenUI.SetActive(true);
    }
    
    public void Fail(){
        float volume = AudioManager.instance.GetVolume("Music");
        AudioManager.instance.SetSound("Music", 0f);
        AudioManager.instance.Play("Lose");
        failScreenUI.SetActive(true);
        StartCoroutine(ReturnSound(5f, "Music", volume));
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

    IEnumerator ReturnSound(float time, string name, float volume)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.SetSound(name, volume);
    }
}
