using UnityEngine;

public class GunChange : MonoBehaviour
{
    //reference to models here..
    private int _gunEXP;
    void Start()
    {
        _gunEXP = 0;
    }

    void Update()
    {
    }

    public void IncreaseGunEXP(int points)
    {
        _gunEXP += points;
    }
}
