using UnityEngine;

public class GunMovement : MonoBehaviour
{
    private Vector3 _initialTouchPosition;
    private Vector3 _initialMousePosition;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private bool isPressed;
    public static bool FirstTouch;
    private float _speed;
    [SerializeField] private GameObject startUI;
    public bool isDead;
    [SerializeField] private AudioSource deathSound;

    private void Start()
    {
        _speed = 4f;
    }

    void Update()
    {
        if (!isDead)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (GameManager.StartGame)
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
            startUI.SetActive(false);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!FirstTouch && touch.position.y > 700)
                {
                    FirstTouch = true;
                }
                else
                {
                    isPressed = true;
                    _initialTouchPosition = touch.position;
                    startPosition = transform.position;
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (FirstTouch)
                {
                    Debug.Log(Input.mousePosition.y);
                    GameManager.StartGame = true;
                }
                isPressed = false;
            }
        }

        if (isPressed)
        {
            var xDelta = (Input.GetTouch(0).position.x - _initialTouchPosition.x) / 100f;
            var newPos = startPosition + new Vector3(xDelta, 0, 0);
            newPos.x = Mathf.Clamp(newPos.x, -3, 3);
            var position = transform.position;
            position = new Vector3(newPos.x, position.y, position.z);
            transform.position = position;
        }
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }
}