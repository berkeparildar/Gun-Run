using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private float range;
    [SerializeField]private Vector3 initPosition;
    [SerializeField] private GunShoot gunScript;
    private const float Speed = 15;

    private void Start()
    {
        gunScript = GameObject.Find("Gun").GetComponent<GunShoot>();
        initPosition = transform.position;
        range = gunScript.GetRange();
    }

    private void Update()
    {
        transform.Translate(Speed * Time.deltaTime * Vector3.forward);
        if (Vector3.Distance(transform.position, initPosition) > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Obstacle")) return;
        other.GetComponent<IPlatformObject>().TakeHit();
        Destroy(this.gameObject);
    }
}
