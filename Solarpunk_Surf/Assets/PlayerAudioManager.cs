using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    private AudioSource[] audioSources;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip dashSound;

    [SerializeField]
    private AudioClip moveLoop;

   
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        audioSources[0].clip = moveLoop;
        audioSources[0].loop = true;
        audioSources[1].clip = dashSound;
        audioSources[2].clip = jumpSound;

    }

    public void playMoveLoop()
    {
        audioSources[0].Play();
    }

    public void pauseMoveLoop()
    {
        audioSources[0].Pause();
    }

    public void boostMoveLoop()
    {
        audioSources[0].pitch = 1.25f;
    }

    public void restoreMoveLoop()
    {
        audioSources[0].pitch = 1.0f;
    }

    public void playDashSound()
    {
        audioSources[1].Play();
    }

    public void playJumpSound()
    {
        audioSources[2].Play();
    }
}
