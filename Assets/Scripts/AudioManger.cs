using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if(instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    void Start(){
        SceneManager.sceneUnloaded += StopTrack;
        SceneManager.sceneLoaded += StartTrack;
        Play("Music");
        Pause("Music");
        Play("Theme");
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Pause(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    public void StopTrack(Scene stopped){
        if(stopped.name == "Level Select"){
            Pause("Select");
        }
        else{
            Pause("Music");
        }
    }
    public void StartTrack(Scene started, LoadSceneMode mode){
        if(started.name == "Level Select"){
            Play("Theme");
        }
        else{
            Sound s = Array.Find(sounds, sound => sound.name == "Music");
            s.source.UnPause();
        }
    }

    /*public void ChangeTrack(Scene prev, Scene curr){
        if (prev.name == "Level Select"){
            Pause("Select");
            Sound s = Array.Find(sounds, sound => sound.name == "Music");
            s.source.UnPause();
        }
        if (curr.name == "Level Select"){
            Pause("Music");
            Play("Theme");
        }
    }*/
}
