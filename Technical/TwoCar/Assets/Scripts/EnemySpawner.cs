using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float delay;
    public List<GameObject> obj;
    public GameObject parent;
 
	void Start ()
	{
	}
	
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
        Transform instance = PoolObject.SpawnObject("Enemy", obj, pos);
        Enemy enemy = instance.GetComponent<Enemy>();
        enemy.Reset();
        enemy.speed = GameManager.Instance.speed;
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
            GameManager.Instance.enemyCount++;
            if (GameManager.Instance.enemyCount%100 == 0 && GameManager.Instance.enemyCount != 0)
            {
                if (GameManager.Instance.velo > 0)
                {
                    GameManager.Instance.velo *= -0.5f;
                }
                else
                {
                    GameManager.Instance.velo *= -2;
                }
                Debug.Log(GameManager.Instance.velo);
            }
            float velo = GameManager.Instance.velo;
            GameManager.Instance.speed += velo;
            GameManager.Instance.minDelay -= velo / 9;
            GameManager.Instance.maxDelay -= velo / 9;
            delay = Random.Range(GameManager.Instance.minDelay, GameManager.Instance.maxDelay);
        }
    }
}
