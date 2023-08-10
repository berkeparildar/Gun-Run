using DG.Tweening;
using TMPro;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public int moneyAmount;
    public int health;
    private TextMeshPro _healthText;
    public GameObject gameOverScreen;
    private GameObject _barrel;
    private BoxCollider _boxCollider;
    private AudioSource _audioSource;

    private void Start()
    {
        gameOverScreen = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        _healthText = transform.GetChild(1).GetComponent<TextMeshPro>();
        _audioSource = GetComponent<AudioSource>();
        moneyAmount = Random.Range(40, 61) + GameManager.IncomeLevel * 10;
        _healthText.text = health.ToString();
        _barrel = transform.GetChild(0).gameObject;
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        _healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            other.GetComponent<GunMovement>().isDead = true;
            GunMovement.firstTouch = false;
            GameManager.StartGame = false;
            var bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (var t in bullets)
            {
                Destroy(t);
            }
            var gunContainer = other.transform.GetChild(1);
            Sequence s = DOTween.Sequence();
            s.Append(gunContainer.DOMoveY(-1, 1).SetRelative().SetEase(Ease.InOutQuad));
            s.Insert(0, gunContainer.DORotate(new Vector3(0, 0, 90), 1).SetEase(Ease.InQuad));
            s.OnComplete(() =>
            {
                gameOverScreen.SetActive(true);
                other.transform.DOKill();
            });
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            _audioSource.Play();
            health -= 2;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(_barrel);
                _healthText.enabled = false;
                _boxCollider.enabled = false;
            }
        }
    }
}