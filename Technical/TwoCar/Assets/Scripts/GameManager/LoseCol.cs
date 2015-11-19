using UnityEngine;
using System.Collections;

public class LoseCol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fuel")
        {
            other.GetComponent<Enemy>().isBlinky = true;
            GameManager.Instance.dieDelay = 1.5f;
            //PoolObject.DespawnObject("Enemy", other.gameObject);
            GameManager.Instance.isDie = true;
        }
        else if (other.tag == "Block")
        {
            PoolObject.DespawnObject("Enemy",other.gameObject);
        }
    }
}
