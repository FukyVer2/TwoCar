using UnityEngine;
using System.Collections;

public class LoseCol : MonoBehaviour
{
    public GameManager gameManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fuel")
        {
            GameManager.Instance.GameOver();
            GameManager.Instance.ClearListGameObject();
        }
        else if (other.tag == "Block")
        {
            GameManager.Instance.RemoveGameObject(other.gameObject);
        }
    }
}
