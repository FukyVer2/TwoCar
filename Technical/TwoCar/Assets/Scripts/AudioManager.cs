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
[System.Serializable]
public class GameAudio
{
    public SoundType sound;
    public AudioClip audioClip;
}

public class AudioManager : MonoSingleton<AudioManager>
{
    public bool isSoundGamePlay = true;

    public List<GameAudio> listAudios;
    private AudioSource audioSource;
	void Start () {

	    audioSource = GetComponent<AudioSource>();
	}

    public void PlaySound(SoundType audioType)
    {
        if (isSoundGamePlay)
        {
            audioSource.PlayOneShot(listAudios[(int) audioType].audioClip);
        }
    }

    //public void Ting(Vector3 position)
    //{
    //    AudioClip ting = listAudios[(int)SoundType.TING].audioClip;
    //    AudioSource.PlayClipAtPoint(ting, position, GameManager.Instance.volume);
    //}

    //public void TurnSound(Vector3 position)
    //{
    //    AudioClip turn = listAudios[(int)SoundType.DRIFT].audioClip;
    //    AudioSource.PlayClipAtPoint(turn, position, GameManager.Instance.volume);
    //}

    //public void Crash(Vector3 position)
    //{
    //    AudioClip crash = listAudios[(int)SoundType.VIBRATE].audioClip;
    //    AudioSource.PlayClipAtPoint(crash, position, GameManager.Instance.volume);
    //}
    //public void Background()
    //{
    //    audio.volume = GameManager.Instance.volume;
    //    audio.Play();
    //}

    //public void StopBackground()
    //{
    //    audio.Stop();        
    //}
}
