public interface IPlatformObject
{
    public float Points{get; set;}
    public float Coefficient{get; set;}
    public GunShoot GunShoot {get; set;}
    public void Perk();
    public void TakeHit();
    public void Die();
}
