using UnityEngine;

public class GunMovement : MonoBehaviour
{
    private Vector3 _initialMousePosition;
    private Vector3 _startPosition;
    private bool _isPressed;
    private float _speed;

    private void Start() 
    {
        _speed = 3f;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {            
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);

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
            float xDelta = Input.mousePosition.x - _initialMousePosition.x;
            xDelta /= 48;
            Vector3 newPos = _startPosition + new Vector3(xDelta, 0, 0);
            newPos.x = Mathf.Clamp(newPos.x, -3, 3);
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
    }
}
