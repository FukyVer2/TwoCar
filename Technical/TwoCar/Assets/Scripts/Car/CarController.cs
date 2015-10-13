using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    public Animator anim;
    public float speed = 2f;
    public GameObject leftPoint;
    public GameObject rightPoint;
    public bool isMove = true;
    public bool isLeft = true;
    public bool isTurn = false;
    public Vector3 target;
	// Use this for initialization
	void Start ()
	{
	    transform.position = leftPoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.A))
	    {
            Turn();
	    }
	    Move();

	    if (transform.position == target)
	    {
            isMove = false;
	        //isTurn = false;
	    }
	}

    [ContextMenu("move")]
    void Turn()
    {

        if (!isMove)
        {

            if (isLeft)
            {
                anim.SetTrigger("isRight");
                target = rightPoint.transform.position;
                isMove = true;
                isLeft = false;
            }
            else if (!isLeft)
            {
                anim.SetTrigger("isLeft");
                target = leftPoint.transform.position;
                isMove = true;
                isLeft = true;
            }
        }
        else if (isMove)
        {
            if (isLeft)
            {
                target = rightPoint.transform.position;
                anim.SetTrigger("isTurn");
                //isTurn = true;

                isLeft = false;
            }
            else
            {
                target = leftPoint.transform.position;
                anim.SetTrigger("isTurn");
                //isTurn = true;

                isLeft = true;
            }
        }
    }

    [ContextMenu("reset")]
    void reset()
    {
        transform.position = leftPoint.transform.position;
        isMove = false;  
    }

    void Reset()
    {
        if (isTurn)
        {
            anim.SetTrigger("isTurn");
        }
    }

    void Move()
    {
        if(isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
    }
}
