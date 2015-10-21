using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    TING = 0,
    DRIFT = 1,
    VIBRATE = 2,
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
	void Start () {
	
	}
	
	void Update () {
	
	}

    public void Ting(Vector3 position)
    {
        AudioClip ting = listAudios[(int)SoundType.TING].audioClip;
        AudioSource.PlayClipAtPoint(ting, position, GameManager.Instance.volume);
    }

    public void TurnSound(Vector3 position)
    {
        AudioClip turn = listAudios[(int)SoundType.DRIFT].audioClip;
        AudioSource.PlayClipAtPoint(turn, position, GameManager.Instance.volume);
    }

    public void Crash(Vector3 position)
    {
        AudioClip crash = listAudios[(int)SoundType.VIBRATE].audioClip;
        AudioSource.PlayClipAtPoint(crash, position, GameManager.Instance.volume);
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
