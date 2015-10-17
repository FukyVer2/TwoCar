using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour
{
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
        float scrollSpeed = GameManager.Instance.speed;
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, size);
        transform.position = startPosition + Vector3.down * newPosition;
       //transform.position += new Vector3(0, -scrollSpeed) * Time.deltaTime;

    }
}
