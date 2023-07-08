using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static readonly float Speed = 10;
    private float _range;
    private Vector3 _initPosition;
    private GunShoot _gunScript;
    void Start()
    {
        _gunScript = GameObject.Find("Gun").GetComponent<GunShoot>();
        _range = _gunScript.GetRange();
        Debug.Log(_range);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _initPosition) > _range)
        {
            Destroy(gameObject);
        }
    }
}
