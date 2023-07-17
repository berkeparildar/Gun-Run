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
            gameOverScreen.SetActive(true);
            GameManager.StartGame = false;
            var bullets = GameObject.FindGameObjectsWithTag("Bullet");
            for (int i = 0; i < bullets.Length; i++)
            {
                Destroy(bullets[i]);   
            }
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