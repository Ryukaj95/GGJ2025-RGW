using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : Singleton<SoundtrackManager>
{
    [SerializeField] private AudioSource source;

    [SerializeField] List<AudioClip> fightSoundtracks = new List<AudioClip>();

    [SerializeField] List<AudioClip> soundtrackQueue = new List<AudioClip>();

    [SerializeField] AudioClip dialogueSoundtrack;

    // Start is called before the first frame update
    void Start()
    {
        GenerateQueue();
    }

    void Update()
    {
        if (!source.isPlaying && soundtrackQueue.Count > 0)
        {
            PlaySoundtrack(soundtrackQueue[0]);
            soundtrackQueue.RemoveAt(0);
        }
    }

    public void PlaySoundtrack(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void PlayFightSoundtrack()
    {
        GenerateQueue();
        PlaySoundtrack(soundtrackQueue[0]);
    }

    public void PlayDialogueSoundtrack()
    {
        if (source.clip == dialogueSoundtrack) return;
        PlaySoundtrack(dialogueSoundtrack);
    }

    public void StopSoundtrack()
    {
        source.Stop();
    }

    public AudioClip GetRandomSoundtrack()
    {
        return fightSoundtracks[Random.Range(0, fightSoundtracks.Count)];
    }

    public void GenerateQueue()
    {
        soundtrackQueue.Clear();
        List<AudioClip> tempSoundtracks = new List<AudioClip>(fightSoundtracks);
        HashSet<AudioClip> usedSoundtracks = new HashSet<AudioClip>();

        while (tempSoundtracks.Count > 0)
        {
            int randomIndex = Random.Range(0, tempSoundtracks.Count);
            AudioClip selectedClip = tempSoundtracks[randomIndex];

            if (!usedSoundtracks.Contains(selectedClip))
            {
                soundtrackQueue.Add(selectedClip);
                usedSoundtracks.Add(selectedClip);
                tempSoundtracks.RemoveAt(randomIndex);
            }
        }
    }
}
