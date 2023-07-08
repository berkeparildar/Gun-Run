using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    [SerializeField] protected static int Speed = 3;
    [SerializeField] protected int hitPoints;
    
    protected void Move()
    {
        transform.Translate(Vector3.down * Speed);
    }
    
    protected void TakeHit()
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            Die();
        }
    }
    
    protected void Perk()
    {
        // Unique perk to apply to weapon here..
    }
    
    protected void Die()
    {
        Destroy(gameObject);
    }
}
