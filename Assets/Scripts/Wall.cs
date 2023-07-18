using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public enum WallPerk
{
    FireRate,
    FireRange,
    GunEXP,
}

public class Wall : MonoBehaviour, IPlatformObject
{
    [SerializeField] private Material greenOpaqueColor;
    [SerializeField] private Material greenTranslucentColor;
    [SerializeField] private Material redOpaqueColor;
    [SerializeField] private Material redTranslucentColor;

    private TextMeshPro _perkText;
    private TextMeshPro _hitPointText;
    private TextMeshPro _sumPointText;

    private WallPerk _perk;
    private GunChange _gunChange;

    //TODO: the level should be decided by the current state of the player
    private int _perkLevel;

    public float Points { get; set; }
    public float SumPoint { get; set; }
    public GunShoot GunShoot { get; set; }

    void Start()
    {
        var gun = GameObject.Find("Gun");
        GunShoot = gun.GetComponent<GunShoot>();
        _gunChange = gun.GetComponent<GunChange>();
        SetPerk();
        SetInitPoints();
        _perkText = transform.GetChild(4).GetComponent<TextMeshPro>();
        _hitPointText = transform.GetChild(5).GetComponent<TextMeshPro>();
        _sumPointText = transform.GetChild(6).GetComponent<TextMeshPro>();
        _perkText.text = _perk.ToString();
        _hitPointText.text = "+" + Points;
        _sumPointText.text = Math.Round(SumPoint, 1).ToString(CultureInfo.InvariantCulture);
    }

    void Update()
    {
        _hitPointText.text = "+" + Math.Round(Points, 1).ToString(CultureInfo.InvariantCulture);;
    }

    public void TakeHit()
    {
        transform
            .DOPunchScale(Vector3.one / 25, 0.3f)
            .OnComplete(() => { DOTween.KillAll(); });
        Points += SumPoint;
    }

    public void Perk()
    {
        switch (_perk)
        {
            case WallPerk.FireRate:
                GunShoot.IncreaseFireRate(Points);
                break;
            case WallPerk.FireRange:
                GunShoot.IncreaseRange(Points);
                break;
            case WallPerk.GunEXP:
                GunChange.IncreaseGunExp(Points);
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
        if (_perk == WallPerk.GunEXP)
        {
            Points = Random.Range(0.1f, 1);
            SumPoint = Random.Range(0.2f, 0.5f);
        }
        else
        {
            Points = (int) Random.Range(5, 10); // * _perklevel maybe?
            SumPoint = Random.Range(0, 1.7f);    
        }
    }

    private void SetPerk()
    {
        var random = Random.Range(1, 4);
        switch (random)
        {
            case 1:
                _perk = WallPerk.FireRate;
                break;
            case 2:
                _perk = WallPerk.FireRange;
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
