using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyablePerks : MonoBehaviour
{
    public GameObject incomeButton;
    public GameObject rangeButton;
    public GameObject yearButton;
    public GameObject rateButton;
    private static int _incomeCost;
    private static int _rangeCost;
    private static int _rateCost;
    private static int _yearCost;
    private TextMeshProUGUI _incomeButtonText;
    private TextMeshProUGUI _rangeButtonText;
    private TextMeshProUGUI _yearButtonText;
    private TextMeshProUGUI _rateButtonText;

    // Start is called before the first frame update
    void Awake()
    {
        _incomeCost = 100;
        _rangeCost = 100;
        _rateCost = 100;
        _yearCost = 100;
        _incomeButtonText = incomeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _rangeButtonText = rangeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _rateButtonText = rateButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _yearButtonText = yearButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PriceCheck();
    }

    private void OnDisable()
    {
        incomeButton.GetComponent<Button>().interactable = true;
        rangeButton.GetComponent<Button>().interactable = true;
        rateButton.GetComponent<Button>().interactable = true;
        yearButton.GetComponent<Button>().interactable = true;
        _incomeButtonText.color = Color.white;
        _rangeButtonText.color = Color.white;
        _rateButtonText.color = Color.white;
        _yearButtonText.color = Color.white;
        _incomeButtonText.text = ButtonText("Income", GameManager.IncomeLevel + 1, _incomeCost);
        _rangeButtonText.text = ButtonText("FireRange", GameManager.RangeLevel + 1, _rangeCost);
        _incomeButtonText.text = ButtonText("FireRate", GameManager.RateLevel + 1, _rateCost);
        _rangeButtonText.text = ButtonText("Year", GameManager.YearLevel + 1, _yearCost);
        
    }

    private string ButtonText(string perk, int level, int price)
    {
        return perk + "\n" + "Level " + level + "\n \n \n \n \n$" + price;
    }

    // Update is called once per frame
    public void IncomeButton()
    {
        GameManager.IncomeLevel++;
        GameManager.Money -= _incomeCost;
        _incomeCost += 100;
        PriceCheck();
    }
    
    public void RangeButton()
    {
        GameManager.RangeLevel++;
        GameManager.Money -= _rangeCost;
        _rangeCost += 100;
        PriceCheck();
    }
    
    public void RateButton()
    {
        GameManager.RateLevel++;
        GameManager.Money -= _rateCost;
        _rateCost += 100;
        PriceCheck();
    }
    
    public void YearButton()
    {
        GameManager.YearLevel++;
        GameManager.Money -= _yearCost;
        _yearCost += 100;
        GunChange.IncreaseGunExp(10 * GameManager.YearLevel);
        PriceCheck();
    }

    private void PriceCheck()
    {
        if (GameManager.Money < _incomeCost)
        {
            incomeButton.GetComponent<Button>().interactable = false;
            _incomeButtonText.color = Color.red;
        }
        
        if (GameManager.Money < _rangeCost)
        {
            rangeButton.GetComponent<Button>().interactable = false;
            _rangeButtonText.color = Color.red;
        }
        
        if (GameManager.Money < _rateCost)
        {
            rateButton.GetComponent<Button>().interactable = false;
            _rateButtonText.color = Color.red;
        }
        
        if (GameManager.Money < _yearCost)
        {
            yearButton.GetComponent<Button>().interactable = false;
            _yearButtonText.color = Color.red;
        }
    }
}
