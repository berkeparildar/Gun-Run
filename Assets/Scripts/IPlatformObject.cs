public interface IPlatformObject
{
    public float Points{get; set;}
    public float SumPoint{get; set;}
    public GunShoot GunShoot {get; set;}
    public void Perk();
    public void TakeHit();
    public void Die();
}
