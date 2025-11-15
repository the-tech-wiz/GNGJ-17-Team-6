using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
        StartCoroutine(DelayedAction(0.5f, () =>{
            Resume();
            SceneController.instance.LoadScene("Level Select");
        }));
    }

    public void Restart(){
        StartCoroutine(DelayedAction(0.5f, () =>{
            Resume();
            SceneController.instance.Reload();
        }));
    }

    public void Exit(){
        StartCoroutine(DelayedAction(0.5f, () =>{
            Application.Quit();
        }));
    }

    public void Click(){
        AudioManager.instance.Play("Button");
    }

    bool isExecuting = false;
    IEnumerator DelayedAction(float time, System.Action task){
        if(isExecuting)
            yield break;
        isExecuting = true;

        yield return new WaitForSecondsRealtime(time);

        task();

        isExecuting = false;
    }
}
