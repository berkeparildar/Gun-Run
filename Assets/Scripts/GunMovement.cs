using UnityEngine;

public class GunMovement : MonoBehaviour
{
    private Vector3 _initialTouchPosition;
    private Vector3 _initialMousePosition;
    private Vector3 _startPosition;
    private bool _isPressed;
    public static bool firstTouch;
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

        if (Input.GetMouseButtonDown(0))
        {
            if (!firstTouch && Input.mousePosition.y > 700)
            {
                firstTouch = true;
            }
            else
            {
                _isPressed = true;
                _initialMousePosition = Input.mousePosition;
                _startPosition = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (firstTouch)
            {
                Debug.Log(Input.mousePosition.y);
                GameManager.StartGame = true;
            }
            _isPressed = false;
        }

        if (_isPressed && firstTouch)
        {
            var xDelta = Input.mousePosition.x - _initialMousePosition.x;
            xDelta /= 100;
            var newPos = _startPosition + new Vector3(xDelta, 0, 0);
            newPos.x = Mathf.Clamp(newPos.x, -3, 3);
            var position = transform.position;
            position = new Vector3(newPos.x, position.y, position.z);
            transform.position = position;
        }
    }
    
    
}