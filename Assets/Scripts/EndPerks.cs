using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPerks : MonoBehaviour
{
    public GameObject incomeButton;
    public GameObject rangeButton;
    private static int _incomeCost;
    private static int _rangeCost;
    private TextMeshProUGUI _incomeButtonText;
    private TextMeshProUGUI _rangeButtonText;

    private void Awake()
    {
        _incomeCost = 100;
        _rangeCost = 100;
        _incomeButtonText = incomeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _rangeButtonText = rangeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PriceCheck();
    }

    private void OnDisable()
    {
        incomeButton.GetComponent<Button>().interactable = true;
        rangeButton.GetComponent<Button>().interactable = true;
        _incomeButtonText.color = Color.white;
        _rangeButtonText.color = Color.white;
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
        _incomeButtonText.text = ButtonText("Income", GameManager.IncomeLevel + 1, _incomeCost);
    }
    
    public void RangeButton()
    {
        GameManager.RangeLevel++;
        GameManager.Money -= _rangeCost;
        _rangeCost += 100;
        PriceCheck();
        _rangeButtonText.text = ButtonText("FireRange", GameManager.RangeLevel + 1, _rangeCost);
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
    }
}
