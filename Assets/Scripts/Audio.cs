using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}
