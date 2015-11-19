using UnityEngine;
using System.Collections;

public class ShakingCamera : MonoBehaviour
{
    public static ShakingCamera Instance;
    public float amtitude = 0.1f;
    public bool isShaking = false;
    private Vector3 _initPos;
    private float time = 1f;
	void Start ()
	{
	    _initPos = transform.localPosition;
	    Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	    if (isShaking)
	    {
            if (time > 0)
            {
                time -= Time.deltaTime;
                transform.localPosition = _initPos + Random.insideUnitSphere * amtitude;
                Handheld.Vibrate();
            }
            else
            {
                StopShaking();
                time = 1f;
            }
	    }
	}

    public void Shake()
    {
        isShaking = true;
    }

    void StopShaking()
    {
        isShaking = false;
        transform.localPosition = _initPos;
    }
}
