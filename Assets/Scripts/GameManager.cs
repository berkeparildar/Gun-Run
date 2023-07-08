using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ammoPrefab;
    public Transform initPosition;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ammoPrefab, initPosition.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
