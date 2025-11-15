using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    public void LoadScene(string sceneName){
        SceneController.instance.LoadScene(sceneName);
    }
}
