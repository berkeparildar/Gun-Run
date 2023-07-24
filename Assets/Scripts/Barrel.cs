using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Barrel : MonoBehaviour
{
    
    public int moneyAmount;
    public int health;
    private TextMeshPro _healthText;
    public GameObject gameOverScreen;
    private GameObject _barrel;
    private BoxCollider _boxCollider;

    private void Start()
    {
        gameOverScreen = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        _healthText = transform.GetChild(1).GetComponent<TextMeshPro>();
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
            GameManager.StartGame = false;
            var bullets = GameObject.FindGameObjectsWithTag("Bullet");
            for (int i = 0; i < bullets.Length; i++)
            {
                Destroy(bullets[i]);   
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
            health--;
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