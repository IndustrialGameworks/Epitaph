using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicAudioController : MonoBehaviour
{

    AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        audioSource.volume = PlayerPrefs.GetFloat("musicVolume", 1);

    }
}
