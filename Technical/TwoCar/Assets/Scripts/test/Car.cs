using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
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
    public GameObject start;
    public GameManager gameManager;

	void Start ()
	{
	    transform.position = start.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.z - (360.0f + rotationTarget) % 360.0f) <= 5.0f)
        {
            rotationTarget = 0;
        }
	    if (!GameManager.Instance.isDie)
	    {
            Move();
            Rotation(rotationTarget);
	    }

	    if (transform.position == targetPos)
	    {
            rotationTarget = 0;
	    }
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

    public void Turn()
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
        AudioManager.Instance.TurnSound(transform.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fuel")
        {
            GameObject fx = Instantiate(other.GetComponent<Enemy>().disappear, other.transform.position, Quaternion.identity) as GameObject;
            GameManager.Instance.AddScore();
            GameManager.Instance.RemoveGameObject(other.gameObject);
            AudioManager.Instance.Ting(transform.position);
            Destroy(other.gameObject);
        }
        else if(other.tag == "Block")
        {
            GameManager.Instance.dieDelay = 1;
            GameManager.Instance.isDie = true;
        }
    }
}
