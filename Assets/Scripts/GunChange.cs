using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunChange : MonoBehaviour
{
    private static float _gunExp;

    private int _firstYearCap = 1800;
    private int _secondYearCap = 1810;
    private int _yearDifference;
    public static int CurrentIndex = 0;
    public GameObject[] guns;
    public Sprite[] gunImages;

    public Image currentImage;
    public Image nextImage;
    public Image fillBar;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI nextText;

    public GameObject floatingMoney;

    private void Start()
    {
        _yearDifference = _secondYearCap - _firstYearCap;
        _gunExp = 0;
    }

    private void Update()
    {
        fillBar.fillAmount = _gunExp / _yearDifference;
        if (fillBar.fillAmount >= 1)
        {
            ChangeGun();
        }
    }

    public static void IncreaseGunExp(float points)
    {
        _gunExp += points;
    }

    public void UpdateUI()
    {
        currentImage.sprite = gunImages[CurrentIndex];
        nextImage.sprite = gunImages[CurrentIndex + 1];
        currentText.text = _firstYearCap.ToString();
        nextText.text = _secondYearCap.ToString();
    }

    public void ChangeGun()
    {
        _firstYearCap = _secondYearCap;
        _secondYearCap += _yearDifference * 2;
        _yearDifference = _secondYearCap - _firstYearCap;
        guns[CurrentIndex].SetActive(false);
        CurrentIndex++;
        if (CurrentIndex < 5)
        {
            guns[CurrentIndex].SetActive(true);
        }

        _gunExp = 0;
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoWallOne"))
        {
            _gunExp += 3;
            other.transform.DOMoveY(-5, 2).SetRelative();
        }
        else if (other.CompareTag("AmmoWallTwo"))
        {
            _gunExp += 7;
            other.transform.DOMoveY(-5, 2).SetRelative();
        }
        else if (other.CompareTag("AmmoWallThree"))
        {
            _gunExp += 12;
            other.transform.DOMoveY(-5, 2).SetRelative();
        }
        else if (other.CompareTag("Money"))
        {
            var money = Instantiate(floatingMoney, transform.position, Quaternion.identity);
            var barrel = other.transform.parent.GetComponent<Barrel>();
            money.GetComponent<TextMeshPro>().text = "+$" + barrel.moneyAmount;
            GameManager.Money += barrel.moneyAmount;
            money.transform.DOMoveY(2, 1).SetRelative().OnComplete(
                () => { Destroy(money); });
        }
    }
}