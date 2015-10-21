using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    TING = 1,
    DRIFT = 2,
    BACKGROUND =3
}


public class AudioManager : MonoSingleton<AudioManager>
{
    [System.Serializable]
    public class GameAudio
    {
        public SoundType sound;
        public AudioClip audioClip;
    }

    public List<GameAudio> listAudios; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Ting(Vector3 position)
    {
        AudioClip ting = listAudios[0].audioClip;
        AudioSource.PlayClipAtPoint(ting, position, GameManager.Instance.volume);
    }

    public void TurnSound(Vector3 position)
    {
        AudioClip turn = listAudios[1].audioClip;
        AudioSource.PlayClipAtPoint(turn, position, GameManager.Instance.volume);
    }

    public void Background()
    {
        audio.volume = GameManager.Instance.volume;
        audio.Play();
    }

    public void StopBackground()
    {
        audio.Stop();        
    }
}
