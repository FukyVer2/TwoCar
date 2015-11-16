using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float delay;
    public List<GameObject> obj;
    public GameObject parent;
    public Vector3 left;
    public Vector3 right;


    void Start()
    {
        
        left = new Vector3(transform.position.x + 0.7f, transform.position.y, 0);
        right = new Vector3(transform.position.x - 0.7f, transform.position.y, 0);
    }

    Vector3 RandomPos()
    {
        int randPos = Random.Range(0, 2);
        if (randPos == 0)
        {
            return left;
        }
        else
        {
            return right;
        }
    }

    void SpawnEnemy()
    {
        int randEnemy = Random.Range(1, 200);
        Vector3 pos = RandomPos();
        if (randEnemy == 111)
        {
             PoolObject.SpawnEnemy( obj[2], pos);
        }
        else
        {
             PoolObject.SpawnEnemy(obj[randEnemy % 2], pos);
        }
    }

    private float timeDelay = 0;
    public void Spawn(float min, float max)
    {
        if (timeDelay >= delay)
        {
            SpawnEnemy();
            SetDelay(min, max);
            GameManager.Instance.ChangeSpeed();
            timeDelay = 0;
        }
        timeDelay += Time.deltaTime;
    }

    public void SetDelay(float min, float max)
    {
        delay = Random.Range(min, max);
    }
}
