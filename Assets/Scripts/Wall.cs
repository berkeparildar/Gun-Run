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
    private Transform _borderTop;
    private Transform _transparentPart;
    private Transform _borderLeft;
    private Transform _borderRight;
    private Transform _sumPointBg;

    private TextMeshPro _perkText;
    private TextMeshPro _hitPointText;
    private TextMeshPro _sumPointText;

    private WallPerk _perk;
    private GunChange _gunChange;
    private int _perkLevel;
    private int _negativeHpChance;
    private int _negativeSpChance;

    private string _hpOperatorSign = "";
    private string _spOperatorSign = "";

    private static float _perkCoolDown;

    public float Points { get; set; }
    public float SumPoint { get; set; }
    public GunShoot GunShoot { get; set; }

    void Start()
    {
        var gun = GameObject.Find("Gun");
        _transparentPart = transform.GetChild(0);
        _borderTop = transform.GetChild(1);
        _borderLeft = transform.GetChild(2);
        _borderRight = transform.GetChild(3);
        _sumPointBg = transform.GetChild(4);
        _negativeHpChance = Random.Range(0, 6);
        _negativeSpChance = Random.Range(0, 6);
        GunShoot = gun.GetComponent<GunShoot>();
        _gunChange = gun.GetComponent<GunChange>();
        SetPerk();
        SetInitPoints();
        SetColor();
        _perkText = transform.GetChild(5).GetComponent<TextMeshPro>();
        _hitPointText = transform.GetChild(6).GetComponent<TextMeshPro>();
        _sumPointText = transform.GetChild(7).GetComponent<TextMeshPro>();
        _perkText.text = _perk.ToString();
        _hitPointText.text = _hpOperatorSign + Points;
        _sumPointText.text = _spOperatorSign + Math.Round(SumPoint, 1).ToString(CultureInfo.InvariantCulture);
    }

    void Update()
    {
        _hitPointText.text = _hpOperatorSign + Math.Round(Points, 1).ToString(CultureInfo.InvariantCulture);
        if (_perkCoolDown >= 0)
        {
            _perkCoolDown -= Time.deltaTime;
        }
        UpdateColor();
    }

    public void TakeHit()
    {
        transform
            .DOPunchScale(Vector3.one / 25, 0.3f);
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
        DOTween.instance.DOKill();
        Destroy(this.gameObject);
    }

    private void SetInitPoints()
    {
        //SO MANY NESTS FIX IN THE FUTURE :(
        if (_perk == WallPerk.GunEXP)
        {
            if (_negativeHpChance == 5)
            {
                //_hpOperatorSign = "-";
                Points = Random.Range(-1, -0.1f);
                if (_negativeSpChance == 5)
                {
                    //_spOperatorSign = "-";
                    SumPoint = Random.Range(-0.5f, -0.2f);
                }
                else
                {
                    _spOperatorSign = "+";
                    SumPoint = Random.Range(0.2f, 0.5f);
                }
            }
            else
            {
                _hpOperatorSign = "+";
                Points = Random.Range(0.1f, 1);
                if (_negativeSpChance == 5)
                {
                   // _spOperatorSign = "-";
                    SumPoint = Random.Range(-0.5f, -0.2f); 
                }
                else
                {
                    _spOperatorSign = "+";
                    SumPoint = Random.Range(0.2f, 0.5f); 
                }
            }
        }
        else
        {
            if (_negativeHpChance == 5)
            {
                //_hpOperatorSign = "-";
                Points = (int)Random.Range(-5, -1);
                if (_negativeSpChance == 5)
                {
                    //_spOperatorSign = "-";
                    SumPoint = Random.Range(-1.7f, -0.3f);
                }
                else
                {
                    _spOperatorSign = "+";
                    SumPoint = Random.Range(0.3f, 1.7f);
                }
            }
            else
            {
                _hpOperatorSign = "+";
                Points = (int)Random.Range(5, 10); // * _perklevel maybe?
                if (_negativeSpChance == 5)
                {
                    //_spOperatorSign = "-";
                    SumPoint = Random.Range(-1.7f, -0.3f);
                }
                else
                {
                    _spOperatorSign = "+";
                    SumPoint = Random.Range(0.3f, 1.7f);
                }
            }
        }
    }

    private void SetColor()
    {
        if (_negativeHpChance == 5)
        {
            _transparentPart.GetComponent<MeshRenderer>().material = redTranslucentColor;
            _borderTop.GetComponent<MeshRenderer>().material = redOpaqueColor;
            _borderLeft.GetComponent<MeshRenderer>().material = redOpaqueColor;
            _borderRight.GetComponent<MeshRenderer>().material = redOpaqueColor;
        }

        if (_negativeSpChance == 5)
        {
            _sumPointBg.GetComponent<MeshRenderer>().material = redOpaqueColor;
        }
    }

    private void UpdateColor()
    {
        // heavy stuff
        if (Points <0)
        {
            _hpOperatorSign = "";
            _transparentPart.GetComponent<MeshRenderer>().material = redTranslucentColor;
            _borderTop.GetComponent<MeshRenderer>().material = redOpaqueColor;
            _borderLeft.GetComponent<MeshRenderer>().material = redOpaqueColor;
            _borderRight.GetComponent<MeshRenderer>().material = redOpaqueColor;
        }
        else
        {
            _hpOperatorSign = "+";
            _transparentPart.GetComponent<MeshRenderer>().material = greenTranslucentColor;
            _borderTop.GetComponent<MeshRenderer>().material = greenOpaqueColor;
            _borderLeft.GetComponent<MeshRenderer>().material = greenOpaqueColor;
            _borderRight.GetComponent<MeshRenderer>().material = greenOpaqueColor;
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
            if (_perkCoolDown <= 0)
            {
                _perkCoolDown = 2;
                Perk();
                Die();
            }
        }
    }
}