using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObject : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource m_audio;

    int currentlyPlaying = 0;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!m_audio.isPlaying)
        {
            m_audio.PlayOneShot(audioClips[currentlyPlaying], 0.2f);
            currentlyPlaying++;
            if (currentlyPlaying > 2)
            {
                currentlyPlaying = 0;
            }
        }
    }
}
