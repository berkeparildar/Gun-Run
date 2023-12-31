using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPerks : MonoBehaviour
{
    public GameObject yearButton;
    public GameObject rateButton;
    private static int _rateCost;
    private static int _yearCost;
    private TextMeshProUGUI _yearButtonText;
    private TextMeshProUGUI _rateButtonText;
    public GunShoot gun;

    private void Awake()
    {
        _rateCost = 100;
        _yearCost = 100;
        _rateButtonText = rateButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _yearButtonText = yearButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PriceCheck();
    }

    private void OnDisable()
    {
        rateButton.GetComponent<Button>().interactable = true;
        yearButton.GetComponent<Button>().interactable = true;
        _rateButtonText.color = Color.white;
        _yearButtonText.color = Color.white;
        
    }

    private string ButtonText(string perk, int level, int price)
    {
        return perk + "\n" + "Level " + level + "\n \n \n \n \n$" + price;
    }

    public void RateButton()
    {
        GameManager.RateLevel++;
        GameManager.Money -= _rateCost;
        _rateCost += 100;
        gun.ResetGun();
        PriceCheck();
        _rateButtonText.text = ButtonText("FireRate", GameManager.RateLevel + 1, _rateCost);
    }
    
    public void YearButton()
    {
        GameManager.YearLevel++;
        GameManager.Money -= _yearCost;
        _yearCost += 100;
        GunChange.IncreaseGunExp(10 * GameManager.YearLevel);
        PriceCheck();
        _yearButtonText.text = ButtonText("Technology", GameManager.YearLevel + 1, _yearCost);
    }

    private void PriceCheck()
    {
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
