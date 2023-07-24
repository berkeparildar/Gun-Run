using System;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    private Vector3 _initialMousePosition;
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

        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
            _initialMousePosition = Input.mousePosition;
            _startPosition = transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isPressed = false;
        }

        if (_isPressed)
        {
            var xDelta = Input.mousePosition.x - _initialMousePosition.x;
            xDelta /= 48;
            var newPos = _startPosition + new Vector3(xDelta, 0, 0);
            newPos.x = Mathf.Clamp(newPos.x, -3, 3);
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
    }
    
    public void Die()
    {
        isDead = true;
    }
}
