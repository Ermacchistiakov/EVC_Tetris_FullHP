using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip brick;
    public AudioClip click;
    public AudioClip success;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySFX(string soundName, float vol)
    {
        if (soundName == "brick") GetComponent<AudioSource>().PlayOneShot(brick, vol);
        if (soundName == "click") GetComponent<AudioSource>().PlayOneShot(click, vol);
        if (soundName == "success") GetComponent<AudioSource>().PlayOneShot(success, vol);
    }
}
