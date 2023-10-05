using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public enum WallPerk
{
    FireRate,
    FireRange,
    Technology,
}

public class Wall : MonoBehaviour, IPlatformObject
{
    [SerializeField] private Material positiveColor;
    [SerializeField] private Material negativeColor;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer titleBg;
    [SerializeField] private SpriteRenderer coefficientBg;
    [SerializeField] private TextMeshPro perkTitleText;
    [SerializeField] private TextMeshPro hitPointText;
    [SerializeField] private TextMeshPro coefficientText;
    [SerializeField] private WallPerk perk;
    [SerializeField] private int negativeHpChance;
    [SerializeField] private int negativeCoefficientChance;
    [SerializeField] private string hpOperatorSign = "";
    [SerializeField] private string coefficientOperatorSign = "";
    private static float _perkCoolDown;
    public float Points { get; set; }
    public float Coefficient { get; set; }
    public GunShoot gunShoot { get; set; }

    private void Start()
    {
        var gun = GameObject.Find("Gun");
        negativeHpChance = Random.Range(0, 6);
        negativeCoefficientChance = Random.Range(0, 6);
        gunShoot = gun.GetComponent<GunShoot>();
        SetPerk();
        SetInitPoints();
        SetColor();
        perkTitleText.text = perk.ToString();
        hitPointText.text = hpOperatorSign + Points;
        coefficientText.text = coefficientOperatorSign + Math.Round(Coefficient, 1).ToString(CultureInfo.InvariantCulture);
    }

    private void Update()
    {
        hitPointText.text = hpOperatorSign + Math.Round(Points, 1).ToString(CultureInfo.InvariantCulture);
        if (_perkCoolDown >= 0)
        {
            _perkCoolDown -= Time.deltaTime;
        }
        UpdateColor();
    }

    public void TakeHit()
    {
        audioSource.Play();
        transform.DOPunchScale(Vector3.one / 25, 0.3f);
        Points += Coefficient;
    }

    public void Perk()
    {
        switch (perk)
        {
            case WallPerk.FireRate:
                gunShoot.IncreaseFireRate(Points);
                break;
            case WallPerk.FireRange:
                gunShoot.IncreaseRange(Points);
                break;
            case WallPerk.Technology:
                GunChange.IncreaseGunExp(Points);
                break;
        }
    }

    public void Die()
    {
        gunShoot.transform.GetComponent<AudioSource>().Play();
        DOTween.Kill(transform);
        DOTween.instance.DOKill();
        Destroy(gameObject);
    }

    private void SetInitPoints()
    {
        if (perk == WallPerk.Technology)
        {
            if (negativeHpChance == 5)
            {
                Points = Random.Range(-1, -0.1f);
                if (negativeCoefficientChance == 5)
                {
                    Coefficient = Random.Range(-0.5f, -0.2f);
                }
                else
                {
                    coefficientOperatorSign = "+";
                    Coefficient = Random.Range(0.2f, 0.5f);
                }
            }
            else
            {
                hpOperatorSign = "+";
                Points = Random.Range(0.1f, 1);
                if (negativeCoefficientChance == 5)
                {
                    Coefficient = Random.Range(-0.5f, -0.2f); 
                }
                else
                {
                    coefficientOperatorSign = "+";
                    Coefficient = Random.Range(0.2f, 0.5f); 
                }
            }
        }
        else
        {
            if (negativeHpChance == 5)
            {
                Points = (int)Random.Range(-5, -1);
                if (negativeCoefficientChance == 5)
                {
                    Coefficient = Random.Range(-1.7f, -0.3f);
                }
                else
                {
                    coefficientOperatorSign = "+";
                    Coefficient = Random.Range(0.3f, 1.7f);
                }
            }
            else
            {
                hpOperatorSign = "+";
                Points = (int)Random.Range(5, 10);
                if (negativeCoefficientChance == 5)
                {
                    Coefficient = Random.Range(-1.7f, -0.3f);
                }
                else
                {
                    coefficientOperatorSign = "+";
                    Coefficient = Random.Range(0.3f, 1.7f);
                }
            }
        }
    }

    private void SetColor()
    {
        if (negativeHpChance == 5)
        {
            titleBg.color = negativeColor.color;
            var currentMats = targetObject.GetComponent<MeshRenderer>().materials;
            currentMats[0] = negativeColor;
            targetObject.GetComponent<MeshRenderer>().materials = currentMats;
            //hitPointText.outlineColor = negativeColor.color;
        }
        else
        {
            titleBg.color = positiveColor.color;
            var currentMats = targetObject.GetComponent<MeshRenderer>().materials;
            currentMats[0] = positiveColor;
            targetObject.GetComponent<MeshRenderer>().materials = currentMats;
            //hitPointText.outlineColor = positiveColor.color;
        }
        
        
        if (negativeCoefficientChance == 5)
        {
            coefficientBg.color = negativeColor.color;
        }
        else
        {
            coefficientBg.color = positiveColor.color;
        }
    }

    private void UpdateColor()
    {
        if (Points <0)
        {
            hpOperatorSign = "";
            titleBg.color = negativeColor.color;
            var currentMats = targetObject.GetComponent<MeshRenderer>().materials;
            currentMats[0] = negativeColor;
            targetObject.GetComponent<MeshRenderer>().materials = currentMats;
        }
        else
        {
            hpOperatorSign = "+";
            titleBg.color = positiveColor.color;
            var currentMats = targetObject.GetComponent<MeshRenderer>().materials;
            currentMats[0] = positiveColor;
            targetObject.GetComponent<MeshRenderer>().materials = currentMats;
        }
    }

    private void SetPerk()
    {
        var random = Random.Range(1, 4);
        switch (random)
        {
            case 1:
                perk = WallPerk.FireRate;
                break;
            case 2:
                perk = WallPerk.FireRange;
                break;
            case 3:
                perk = WallPerk.Technology;
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