using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGate : MonoBehaviour {

    public AudioClip openSound;
    public AudioClip closeSound;
    AudioSource source;

    private void Start()
    {
        source = GetComponentInChildren<AudioSource>();
        if (source == null)
            Destroy(this);
    }

    public void PlayGateAudio(bool open) {

        if (!source.isPlaying)
        {
            if (open)
                source.clip = closeSound;
            else
                source.clip = openSound;
            source.Play();

        }

    }


}
