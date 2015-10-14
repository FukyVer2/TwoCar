using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    public Vector3 startPosition;
    public float size;
	// Use this for initialization
	void Start ()
	{
	    startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, size);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}
