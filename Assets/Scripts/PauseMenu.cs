using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public InputAction Trigger;
    
    void Start(){
        Trigger.Enable();
    }

    void Update()
    {
        if(Trigger.triggered){
            if(gameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneController.instance.LoadScene("Level Select");
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneController.instance.Reload();
    }

    public void Exit(){
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void Click(){
        AudioManager.instance.Play("Button");
    }
}
