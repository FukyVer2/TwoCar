using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PoolObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static Transform SpawnObject(string poolName, GameObject obj, Vector3 pos)
    {
        SpawnPool pool = PoolManager.Pools[poolName];
        Transform gameObj = pool.Spawn(obj, pos, Quaternion.identity);
        //Enemy enemy = gameObj.GetComponent<Enemy>();
        return gameObj;
    }

    public static void DespawnObject(string poolName, GameObject obj)
    {
        SpawnPool pool = PoolManager.Pools[poolName];
        pool.Despawn(obj.transform);
    }

    public static void DespawnAll(string poolName)
    {
        SpawnPool pool = PoolManager.Pools[poolName];
        pool.DespawnAll();
    }
}
