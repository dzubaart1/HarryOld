using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource _disapearItemSound;
    [SerializeField] private List<AudioSource> _backgroundSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (!_backgroundSounds.Any(sound => sound.isPlaying))
        {
            PlayRandomBackgroundSound();
        }
    }

    public void PlayDisapearItemSound(Vector3 pos)
    {
        _disapearItemSound.transform.position = pos;
        _disapearItemSound.Play();
    }

    private void PlayRandomBackgroundSound()
    {
        if (_backgroundSounds.Count == 0)
        {
            return;
        }
        
        int randID = Random.Range(0, _backgroundSounds.Count);
        
        _backgroundSounds[randID].Play();
    }
}
