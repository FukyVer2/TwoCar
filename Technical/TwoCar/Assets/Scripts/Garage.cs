using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Garage : MonoBehaviour
{
    public List<GameObject> car;
    public List<GameObject> button; 
    public int number = 0;
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void CarOne()
    {
        number = 0;
    }

    public void CarTwo()
    {
        number = 1;
    }

    public void ChangeCar(GameObject clickedButton)
    {
        car[number].GetComponent<SpriteRenderer>().sprite = clickedButton.GetComponent<Image>().sprite;
        button[number].GetComponent<Image>().sprite = clickedButton.GetComponent<Image>().sprite;
    }
}
