using DG.Tweening;
using TMPro;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    
    private int _moneyAmount;
    public int health;
    private TextMeshPro _healthText;
    public GameObject gameOverScreen;

    private void Start()
    {
        gameOverScreen = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        _healthText = transform.GetChild(4).GetComponent<TextMeshPro>();
        _moneyAmount = 15;
        _healthText.text = health.ToString();
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
            //gunContainer.DOMoveY(-1, 1).SetRelative();
            //gunContainer.DORotate(new Vector3(0, 0, 90), 1)
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
                GameManager.Money += _moneyAmount;
                Destroy(this.gameObject);
            }
        }
    }
}