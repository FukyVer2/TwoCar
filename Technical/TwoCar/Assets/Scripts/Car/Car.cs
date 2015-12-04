using UnityEngine;

public class Car : MonoBehaviour {
    public float rotationLeft;
    public float rotationRight;
    public float speedRotation;
    public float speedMove;
    private float _rotationTarget = 0;
    public GameObject left;
    public GameObject right;
    public bool isLeft;
    public bool isMove = false;
    public Vector3 targetPos;
    public GameObject start;
    public SpriteRenderer image;
    public bool isBlinky = false;
    public float duration = 1.25f;
    public int time;

	void Start ()
	{ 
	    transform.position = start.transform.position;
        image = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update ()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.z - (360.0f + _rotationTarget) % 360.0f) <= 5.0f)
        {
            _rotationTarget = 0;
        }
	    if (!GameManager.Instance.isDie && !GameManager.Instance.isPause)
	    {
            Move();
            Rotation(_rotationTarget);
	    }

	    if (transform.position == targetPos)
	    {
            _rotationTarget = 0;
	    }

        if (isBlinky)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
                Blinky();
            }
            else
            {
                image.enabled = true;
                isBlinky = false;
                duration = 1.25f;
                time = 0;
            }
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
            _rotationTarget = rotationRight;
            isMove = true;
            targetPos = right.transform.position;
            isLeft = false;
        }
        else
        {
            _rotationTarget = rotationLeft;
            isMove = true;
            targetPos = left.transform.position;
            isLeft = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fuel")
        {
            PoolObject.SpawnObject("Effect", other.GetComponent<Enemy>().disappear, other.transform.position);
            ScoreManager.Instance.AddGold();
            PoolObject.DespawnObject("Enemy",other.gameObject);
            //AudioManager.Instance.Ting(transform.position);
            AudioManager.Instance.PlaySound(SoundType.TING);
        }
        else if(other.tag == "Block")
        {
            isBlinky = true;
            AudioManager.Instance.PlaySound(SoundType.VIBRATE);
            GameManager.Instance.dieDelay = 1.25f;
            GameManager.Instance.isDie = true;
            ShakingCamera.Instance.Shake();
        }
        else if (other.tag == "Energy")
        {
            ScoreManager.Instance.AddDiamon(1);
            PoolObject.SpawnObject("Effect", other.GetComponent<Enemy>().disappear, other.transform.position);
            PoolObject.DespawnObject("Enemy",other.gameObject);
            AudioManager.Instance.PlaySound(SoundType.TING);
        }
    }

    void Blinky()
    {
        if (image.color.a > 0 && time < 15 && time >= 0)
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
}
