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
        _initPosition = transform.position;
        _range = _gunScript.GetRange();
        Debug.Log(_range);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed * Time.deltaTime * Vector3.forward);
        if (Vector3.Distance(transform.position, _initPosition) > _range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            other.GetComponent<IPlatformObject>().TakeHit();
            Destroy(this.gameObject);
        }
    }
}
