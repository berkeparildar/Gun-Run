using DG.Tweening;
using TMPro;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private int moneyAmount;
    [SerializeField] private int health;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject barrel;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private AudioSource audioSource;
    public int MoneyAmount => moneyAmount;

    private void Start()
    {
        gameOverScreen = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        moneyAmount = Random.Range(40, 61) + GameManager.IncomeLevel * 10;
        healthText.text = health.ToString();
    }

    private void Update()
    {
        healthText.text = health.ToString();
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
            audioSource.Play();
            transform.GetChild(0).DOPunchScale(Vector3.up / 25, 0.3f);
            health --;
            Destroy(other.gameObject);
            if (health > 0) return;
            DOTween.Kill(transform);
            DOTween.instance.DOKill(transform);
            Destroy(barrel);
            healthText.enabled = false;
            boxCollider.enabled = false;
        }
    }
}