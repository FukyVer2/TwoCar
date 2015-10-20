using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float delay;
    //public float minDelay;
    //public float maxDelay;
    //public float speed = 4;
    public List<GameObject> obj;
    public float velo = 0.01f;
    public GameObject parent;
 
	// Use this for initialization
	void Start ()
	{
	    delay = Random.Range(2f, 5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (!GameManager.Instance.isPause)
	    {
            Spawn();
	    }
	}

    GameObject RandomObject()
    {
        int randEnemy = Random.Range(1, 10);
        return obj[randEnemy%2];
    }

    Vector3 RandomPos()
    {
        int randPos = Random.Range(1, 10);
        if (randPos%2 == 0)
        {
            return new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z);
        }
        else
        {
            return new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z);
        }
    }

    void SpawnEnemy()
    {
        GameObject obj = RandomObject();
        Vector3 pos = RandomPos();
        //GameObject instance =  Instantiate(obj, pos, Quaternion.identity) as GameObject;
        Transform instance = PoolObject.SpawnObject("Enemy", obj, pos);
        //instance.transform.SetParent(parent.transform);
        Enemy enemy = instance.GetComponent<Enemy>();
        enemy.speed = GameManager.Instance.speed;
        //GameManager.Instance.AddGameObject(instance);
    }

    void Spawn()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            GameManager.Instance.speed += velo;
            GameManager.Instance.minDelay -= velo / 9;
            GameManager.Instance.maxDelay -= velo / 9;
            delay = Random.Range(GameManager.Instance.minDelay, GameManager.Instance.maxDelay);
        }
    }
}
