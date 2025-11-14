using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    //public InputAction Trigger;
    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.anyKey.isPressed){
            Debug.Log("Triggered");
            gameObject.SetActive(false);
        }
    }

    public void Remove(){
        gameObject.SetActive(false);
    }
}
