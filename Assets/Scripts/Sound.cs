using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;

	[Range(0f, 2f)]
	public float volume;
	public bool loop;

   [HideInInspector]
   public AudioSource source;
}
