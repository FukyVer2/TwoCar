using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Garage : MonoSingleton<Garage>
{
    public List<GameObject> car;
    public List<GameObject> button;
    public List<GameObject> buyCar;
    public List<GameObject> unlock;
    public List<Text> priceText; 
    public Image button1;
    public Image button2;
    public Color disableColor = new Color(1,1,1,0.5f);
    public bool[] isBuy;
    public int[] price;
    public int number = 0;

	void Start ()
	{
        isBuy = new bool[unlock.Count];
	    if (!PlayerPrefs.HasKey("Buy"))
	    {
	        PlayerPrefsX.SetBoolArray("Buy", isBuy);
	    }
        price = new []{100, 500, 1000,5000,10000, 20000, 40000};
	    isBuy = PlayerPrefsX.GetBoolArray("Buy");
        button2.color = disableColor;
        CheckUnlock();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void CheckUnlock()
    {
        for (int i = 0; i < unlock.Count; i++)
        {
            unlock[i].SetActive(!isBuy[i]);
            priceText[i].text = "" + price[i];
        }
    }

    public void CarOne()
    {
        number = 0;
        button2.color = disableColor;
        button1.color = new Color(1, 1, 1, 1);
    }

    public void CarTwo()
    {
        number = 1;
        button1.color = disableColor;
        button2.color = new Color(1, 1, 1, 1);
    }

    public void ChangeCar(GameObject clickedButton)
    {
        car[number].GetComponent<SpriteRenderer>().sprite = clickedButton.GetComponent<Image>().sprite;
        button[number].GetComponent<Image>().sprite = clickedButton.GetComponent<Image>().sprite;
    }

    public void Unlock(GameObject clickedButton)
    {
        int index = unlock.IndexOf(clickedButton);
        if (ScoreManager.Instance.gold >= price[index])
        {
            isBuy[index] = true;
            clickedButton.SetActive(false);
            ScoreManager.Instance.gold -= price[index];
            PlayerPrefsX.SetBoolArray("Buy", isBuy);
            PlayerPrefs.SetInt("Gold", ScoreManager.Instance.gold);
            ScoreManager.Instance.ShowGold();
        }
    }
}
