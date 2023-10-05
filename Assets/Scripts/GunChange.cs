using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunChange : MonoBehaviour
{
    private static float _technologyLevel;
    private int _firstYearCap = 0;
    private int _secondYearCap = 10;
    private int _yearDifference;
    public static int CurrentIndex;
    public GameObject[] guns;
    public Sprite[] gunImages;
    public Image nextImage;
    public Image fillBar;
    public TextMeshProUGUI technologyLevelText;
    public GameObject floatingMoney;
    private GameObject _ammoWallOne;
    private GameObject _ammoWallTwo;
    private GameObject _ammoWallThree;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _yearDifference = _secondYearCap - _firstYearCap;
        _technologyLevel = 0;
    }

    private void Update()
    {
        fillBar.fillAmount = _technologyLevel / _yearDifference;
        if (fillBar.fillAmount >= 1)
        {
            ChangeGun();
        }
        UpdateUI();
    }

    public static void IncreaseGunExp(float points)
    {
        _technologyLevel += points;
    }

    private void UpdateUI()
    {
        nextImage.sprite = gunImages[CurrentIndex + 1];
        technologyLevelText.text = ((int)_technologyLevel).ToString();
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
        _technologyLevel = 0;
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        _ammoWallOne = GameObject.FindWithTag("AmmoWallOne");
        _ammoWallTwo = GameObject.FindWithTag("AmmoWallTwo");
        _ammoWallThree = GameObject.FindWithTag("AmmoWallThree");
        if (other.CompareTag("AmmoWallOne"))
        {
            _audioSource.Play();
            _technologyLevel += 3;
            other.transform.DOMoveY(-5, 2).SetRelative();
            _ammoWallTwo.GetComponent<BoxCollider>().enabled = false;
            _ammoWallThree.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("AmmoWallTwo"))
        {
            _audioSource.Play();
            _technologyLevel += 7;
            other.transform.DOMoveY(-5, 2).SetRelative();
            _ammoWallOne.GetComponent<BoxCollider>().enabled = false;
            _ammoWallThree.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("AmmoWallThree"))
        {
            _audioSource.Play();
            _technologyLevel += 12;
            other.transform.DOMoveY(-5, 2).SetRelative();
            _ammoWallTwo.GetComponent<BoxCollider>().enabled = false;
            _ammoWallThree.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Money"))
        {
            _audioSource.Play();
            var money = Instantiate(floatingMoney, transform.position, Quaternion.Euler(45, 0, 0));
            var barrel = other.transform.parent.GetComponent<Barrel>();
            money.GetComponent<TextMeshPro>().text = "+$" + barrel.MoneyAmount;
            GameManager.Money += barrel.MoneyAmount;
            money.transform.DOMove(new Vector3(0, 2, 2), 1).SetRelative().OnComplete(
                () => { Destroy(money); });
            Destroy(other.gameObject);
        }
    }
}