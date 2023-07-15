using UnityEngine;

public class GunChange : MonoBehaviour
{
    //reference to models here..
    private float _gunEXP;
    void Start()
    {
        _gunEXP = 0;
    }

    void Update()
    {
    }

    public void IncreaseGunEXP(float points)
    {
        _gunEXP += points;
    }
}
