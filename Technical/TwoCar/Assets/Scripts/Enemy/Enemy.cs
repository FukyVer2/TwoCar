using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour 
{
    public GameObject disappear;
    public SpriteRenderer image;
    public float speed = 5;
    public bool isBlinky = false;
    public float blinkyDuration = 1.25f;
    public int time = 0;
	void Start ()
	{
	    image = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () 
    {
        if (!GameManager.Instance.isPause)
        {
            Move();
        }
        if (isBlinky)
        {
#if UNITY_EDITOR
            //Debug.Log("blinky");
#endif

            if (blinkyDuration > 0)
            {
                Blinky();
                blinkyDuration -= Time.deltaTime;
            }
            else
            {
                image.enabled = true;
                isBlinky = false;
                blinkyDuration = 1.25f;
                time = 0;
            }
        }
    }

    void Move()
    {
        transform.position += new Vector3(0, -speed) * Time.deltaTime; 
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
    }
}
