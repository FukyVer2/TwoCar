using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class BuyButton
{
    public Button activeCar;
    public Image skinCar;
    public Image Lock;
    public Text priceText;
    public Button buyButton;
    public Text buttonText;
    public bool isBuy;
}
public class Garage : MonoSingleton<Garage>
{
    

    public GameObject notification;
    public List<SpriteRenderer> car;
    public List<Image> button;
    public List<BuyButton> listBuyButtons;
    public Image button1;
    public Image button2;
    public Color disableColor = new Color(1,1,1,0.5f);
    public bool[] isBuy;
    public int[] price;
    public int number = 0;
    public int preIndex;

	void Start ()
	{
        isBuy = new bool[listBuyButtons.Count];
	    isBuy[0] = true;
        //PlayerPrefsX.SetBoolArray("Buy", isBuy);
        if (!PlayerPrefs.HasKey("Buy"))
	    {
	        PlayerPrefsX.SetBoolArray("Buy", isBuy);
	    }
        price = new []{100, 1000, 5000,15000,25000, 40000, 100000};
	    isBuy = PlayerPrefsX.GetBoolArray("Buy");
        button2.color = disableColor;
        CheckUnlock();
	}

    public void CheckUnlock()
    {
        for (int i = 0; i < listBuyButtons.Count; i++)
        {
            listBuyButtons[i].activeCar.enabled = isBuy[i];
            listBuyButtons[i].Lock.enabled = !isBuy[i];
            listBuyButtons[i].priceText.text = "" + price[i];
            listBuyButtons[i].buyButton.enabled = !isBuy[i];
            listBuyButtons[i].isBuy = isBuy[i];
            if (!isBuy[i])
            {
                listBuyButtons[i].buttonText.text = "Buy";
                
            }
            else
            {
                listBuyButtons[i].buttonText.text = "Owned";
            }
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

    public void ChangeCar(Button clickedButton)
    {
        int index = -1;
        for (int i = 0; i < listBuyButtons.Count; i++)
        {
            if (listBuyButtons[i].activeCar == clickedButton)
            {
                index = i;
                break;
            }
        }
        car[number].sprite = listBuyButtons[index].skinCar.sprite;
        button[number].sprite = listBuyButtons[index].skinCar.sprite;
    }

    public void Unlock()
    {
        notification.SetActive(false);
        listBuyButtons[preIndex].isBuy = true;
        isBuy[preIndex] = true;
        listBuyButtons[preIndex].Lock.enabled = false;
        listBuyButtons[preIndex].activeCar.enabled = true;
        listBuyButtons[preIndex].buttonText.text = "Owned";
        listBuyButtons[preIndex].buyButton.enabled = false;
        PlayerPrefsX.SetBoolArray("Buy", isBuy);
        ScoreManager.Instance.gold -= price[preIndex];
        ScoreManager.Instance.ShowGold();
    }

    public void Notify(Button clickedButton)
    {
        for (int i = 0; i < listBuyButtons.Count; i++)
        {
            if (listBuyButtons[i].buyButton == clickedButton)
            {
                preIndex = i;
                break;
            }
        }
        if (ScoreManager.Instance.gold >= price[preIndex])
        {
            notification.SetActive(true);
        }
        else
        {
            GameManager.Instance.ShowNotEnough();
        }
    }

    public void CloseNotify()
    {
        notification.SetActive(false);
    }
}
