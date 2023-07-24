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
    public static int CurrentIndex;
    public GameObject[] guns;
    public Sprite[] gunImages;

    public Image currentImage;
    public Image nextImage;
    public Image fillBar;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI nextText;

    public GameObject floatingMoney;
    private GameObject _ammoWallOne;
    private GameObject _ammoWallTwo;
    private GameObject _ammoWallThree;

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

    private void UpdateUI()
    {
        currentImage.sprite = gunImages[CurrentIndex];
        nextImage.sprite = gunImages[CurrentIndex + 1];
        currentText.text = _firstYearCap.ToString();
        nextText.text = _secondYearCap.ToString();
    }

    private void ChangeGun()
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
        _ammoWallOne = GameObject.FindWithTag("AmmoWallOne");
        _ammoWallTwo = GameObject.FindWithTag("AmmoWallTwo");
        _ammoWallThree = GameObject.FindWithTag("AmmoWallThree");
        if (other.CompareTag("AmmoWallOne"))
        {
            _gunExp += 3;
            other.transform.DOMoveY(-5, 2).SetRelative();
            _ammoWallTwo.GetComponent<BoxCollider>().enabled = false;
            _ammoWallThree.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("AmmoWallTwo"))
        {
            _gunExp += 7;
            other.transform.DOMoveY(-5, 2).SetRelative();
            _ammoWallOne.GetComponent<BoxCollider>().enabled = false;
            _ammoWallThree.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("AmmoWallThree"))
        {
            _gunExp += 12;
            other.transform.DOMoveY(-5, 2).SetRelative();
            _ammoWallTwo.GetComponent<BoxCollider>().enabled = false;
            _ammoWallThree.GetComponent<BoxCollider>().enabled = false;
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