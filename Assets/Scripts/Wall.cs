using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public enum WallPerk
{
    FireRate,
    Range,
    GunEXP,
}

public class Wall : MonoBehaviour, IPlatformObject
{
    [SerializeField]
    private Material greenOpaqueColor;
    [SerializeField]
    private Material greenTranslucentColor;
    [SerializeField]
    private Material redOpaqueColor;
    [SerializeField]
    private Material redTranslucentColor;

    private TextMeshPro _perkText;
    private TextMeshPro _hitPointText;

    private WallPerk _perk;
    private GunChange _gunChange;

    //TODO: the level should be decided by the current state of the player
    private int _perkLevel;

    public int Points { get; set; }
    public int SumPoint { get; set; }
    public GunShoot GunShoot { get; set; }

    void Start()
    {
        var gun = GameObject.Find("Gun");
        GunShoot = gun.GetComponent<GunShoot>();
        _gunChange = gun.GetComponent<GunChange>();
        SetInitPoints();
        SetPerk();
        _perkText = transform.GetChild(4).GetComponent<TextMeshPro>();
        _hitPointText = transform.GetChild(5).GetComponent<TextMeshPro>();
        _perkText.text = _perk.ToString();
        _hitPointText.text = "+" + Points;
    }

    void Update()
    {
        _hitPointText.text = "+" + Points;
    }

    public void TakeHit()
    {
        transform
            .DOPunchScale(Vector3.one / 25, 0.3f)
            .OnComplete(() =>
            {
                DOTween.KillAll();
            });
        Points += SumPoint;
    }

    public void Perk()
    {
        switch (_perk)
        {
            case WallPerk.FireRate:
                GunShoot.IncreaseFireRate(Points);
                break;
            case WallPerk.Range:
                GunShoot.IncreaseRange(Points);
                break;
            case WallPerk.GunEXP:
                _gunChange.IncreaseGunEXP(Points);
                break;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

     private void SetInitPoints()
    {
        //TODO: the hitpoint should be decided by the current state of the player
        Points = Random.Range(5, 10); // * _perklevel maybe?
        SumPoint = 1;
    }

    private void SetPerk()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 1:
                _perk = WallPerk.FireRate;
                break;
            case 2:
                _perk = WallPerk.Range;
                break;
            case 3:
                _perk = WallPerk.GunEXP;
                break;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            Perk();
            Die();
        }
    }
}
