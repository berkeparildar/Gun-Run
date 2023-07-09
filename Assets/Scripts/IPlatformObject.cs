using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlatformObject
{
    public int Points{get; set;}
    public int SumPoint{get; set;}
    public GunShoot GunShoot {get; set;}
    public void Perk();
    public void TakeHit();
    public void Die();
}
