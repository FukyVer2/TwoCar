using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed;
    public GameManager gameManager;
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (!GameManager.Instance.isPause)
	    {
            Move();
	    }
        DestroyObject();
	}

    void Move()
    {
        gameObject.transform.position += new Vector3(0, -speed) * Time.deltaTime; 
    }

    void DestroyObject()
    {
        if (transform.position.y < Camera.main.ScreenToWorldPoint(Camera.main.transform.position).y)
        {
            Destroy(gameObject);
        }
    }
}
