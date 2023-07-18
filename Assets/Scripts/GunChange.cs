using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunChange : MonoBehaviour
{
    //reference to models here..
    private static float _gunExp;
    
    private int _firstYearCap = 1800;
    private int _secondYearCap = 1810;
    private int _yearDifference;
    private int _currentIndex = 0;
    public GameObject[] guns;
    public Sprite[] gunImages;

    public Image currentImage;
    public Image nextImage;
    public Image fillBar;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI nextText;
    
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
        currentImage.sprite = gunImages[_currentIndex];
        nextImage.sprite = gunImages[_currentIndex + 1];
        currentText.text = _firstYearCap.ToString();
        nextText.text = _secondYearCap.ToString();
    }

    public void ChangeGun()
    {
        _firstYearCap = _secondYearCap;
        _secondYearCap += _yearDifference * 2;
        _yearDifference = _secondYearCap - _firstYearCap;
        guns[_currentIndex].SetActive(false);
        _currentIndex++;
        if (_currentIndex < 5)
        {
            guns[_currentIndex].SetActive(true);
        }

        _gunExp = 0;
        UpdateUI();
    }
}
