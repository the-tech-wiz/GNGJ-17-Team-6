using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    void Start(){
        StartCoroutine(Activate(5f));
    }

    public GameObject title;
    public GameObject anyKey;

    IEnumerator Activate(float time)
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(time);
        Debug.Log("Execute");
        title.SetActive(true);
        anyKey.SetActive(true);
    }

    //public InputAction Trigger;
    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.anyKey.isPressed){
            Debug.Log("Triggered");
            AudioManager.instance.Pause("Theme");
            AudioManager.instance.Play("Select");
            gameObject.SetActive(false);
        }
    }

    public void Remove(){
        AudioManager.instance.Pause("Theme");
        AudioManager.instance.Play("Select");
        gameObject.SetActive(false);
    }
}
