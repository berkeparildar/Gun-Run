using UnityEngine;

public class GunMovement : MonoBehaviour
{
    private Vector3 _initialTouchPosition;
    private Vector3 _startPosition;
    private bool _isPressed;
    private float _speed;
    public GameObject startUI;
    public bool isDead;

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
                _isPressed = true;
                _initialTouchPosition = touch.position;
                _startPosition = transform.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isPressed = false;
            }
        }

        if (_isPressed)
        {
            var xDelta = (Input.GetTouch(0).position.x - _initialTouchPosition.x) / 48f;
            var newPos = _startPosition + new Vector3(xDelta, 0, 0);
            newPos.x = Mathf.Clamp(newPos.x, -3, 3);
            var position = transform.position;
            position = new Vector3(newPos.x, position.y, position.z);
            transform.position = position;
        }
    }

    public void Die()
    {
        isDead = true;
    }
}