using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed;
    public GameManager gameManager;
    public GameObject disappear;
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (!GameManager.Instance.isPause)
	    {
            Move();
	    }
	}

    void Move()
    {
        gameObject.transform.position += new Vector3(0, -speed) * Time.deltaTime; 
    }
}
