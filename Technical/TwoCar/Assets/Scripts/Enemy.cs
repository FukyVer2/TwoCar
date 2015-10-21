using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour 
{
    public float speed;
    public GameObject disappear;
    public GameObject explorer;
    public SpriteRenderer image;
    public bool isBlinky = false;
    public int time = 0;
	void Start ()
	{
	    image = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (!GameManager.Instance.isPause)
	    {
            Move();
	    }
	    if (GameManager.Instance.isDie && isBlinky)
	    {
            Blinky();
	    }
	}

    void Move()
    {
        gameObject.transform.position += new Vector3(0, -speed) * Time.deltaTime; 
    }

    void Blinky()
    {
        if (image.color.a > 0 && time < 15 && time >=0)
        {
            image.enabled = false;
            time++;
            if (time >= 15)
            {
                time = -15;
            }
        }
        else
        {
            image.enabled = true;
            time++;
        }
    }

    public void Reset()
    {
        speed = 0;
        isBlinky = false;
        time = 0;
    }
}
