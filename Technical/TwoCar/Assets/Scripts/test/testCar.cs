using UnityEngine;
using System.Collections;

public class testCar : MonoBehaviour {
    public float rotationLeft;
    public float rotationRight;
    public float speedRotation;
    public float speedMove;
    private float rotationTarget = 0;
    public GameObject left;
    public GameObject right;
    public bool isLeft;
    public bool isMove = false;
    public Vector3 targetPos;


	void Start ()
	{
	    transform.position = left.transform.position;
	    isLeft = true;
	    targetPos = right.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
           Turn();
        }

        if (Mathf.Abs(transform.rotation.eulerAngles.z - (360.0f + rotationTarget) % 360.0f) <= 5.0f)
        {
            rotationTarget = 0;
        }

        Rotation(rotationTarget);
	    Move();
	}

    void Rotation(float rotationTarget)
    {
        
        Quaternion target = Quaternion.Euler(0, 0, rotationTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, speedRotation);
    }

    void Move()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speedMove);
        }
    }

    void Turn()
    {
        if (isLeft)
        {
            rotationTarget = rotationRight;
            isMove = true;
            targetPos = right.transform.position;
            isLeft = false;
        }
        else
        {
            rotationTarget = rotationLeft;
            isMove = true;
            targetPos = left.transform.position;
            isLeft = true;
        }
    }
}
