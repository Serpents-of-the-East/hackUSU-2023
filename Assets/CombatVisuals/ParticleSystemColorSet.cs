using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemColorSet : MonoBehaviour
{
    [Header("Elements Colors")]
    public Color fire;
    public Color ice;
    public Color lightning;
    public Color wind;
    public Color dark;
    public Color lightColor;
    public Color poison;
    public Color water;
    public Color ground;

    [Header("Particle Systems")]
    public ParticleSystem magicCharge;
    public ParticleSystem magicAttack;
    public ParticleSystem physCharge;
    public ParticleSystem physAttack;

    public enum ParticleElementType
    {
        fire,
        ice,
        lightning,
        wind,
        dark,
        light,
        ground,
        poison,
        water,
    }

    public enum ParticleAttackType
    {
        physical,
        elemental
    }

    private void SetDamage(Color color, ParticleAttackType particleAttackType)
    {
        if (particleAttackType == ParticleAttackType.physical)
        {
            if (physAttack != null)
            {
                var main = physAttack.main;
                main.startColor = color;
            }
            if (physCharge != null)
            {
                var main2 = physCharge.main;
                main2.startColor = color;
            }
        }
        else
        {
            if (magicAttack != null)
            {
                var main = magicAttack.main;
                main.startColor = color;
            }

            if (magicCharge != null)
            {
                var main2 = magicCharge.main;
                main2.startColor = color;
            }
        }

    }

    public void SetElement(ParticleElementType particleElementType, ParticleAttackType particleAttackType)
    {
        switch (particleElementType)
        {
            case ParticleElementType.fire:
                SetDamage(fire, particleAttackType);
                break;
            case ParticleElementType.ice:
                SetDamage(ice, particleAttackType);
                break;
            case ParticleElementType.lightning:
                SetDamage(lightning, particleAttackType);
                break;
            case ParticleElementType.wind:
                SetDamage(wind, particleAttackType);
                break;
            case ParticleElementType.light:
                SetDamage(lightColor, particleAttackType);
                break;
            case ParticleElementType.ground:
                SetDamage(ground, particleAttackType);
                break;
            case ParticleElementType.poison:
                SetDamage(poison, particleAttackType);
                break;
            case ParticleElementType.water:
                SetDamage(water, particleAttackType);
                break;
        }
    }
}
