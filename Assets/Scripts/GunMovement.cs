using UnityEngine;

public class GunMovement : MonoBehaviour
{
    private Vector3 _initialMousePosition;
    private Vector3 _startPosition;
    private bool _isPressed;
    
    protected void Start()
    {
    }

    protected void Update()
    {
        Movement();
    }

    private void Movement()
    {
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
            if (newPos.x > 3)
            {
                newPos.x = 3;
            }
            else if (newPos.x < -3)
            {
                newPos.x = -3;
            }
            else
            {
                transform.position = newPos;
            }
        }
    }
}
