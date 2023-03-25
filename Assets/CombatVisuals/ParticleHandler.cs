using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParticleHandler : MonoBehaviour
{

    public ParticleSystem magicCharging;
    public ParticleSystem magicAttack;
    public ParticleSystem physCharging;
    public ParticleSystem physAttack;

    public AudioSource charging;
    public AudioSource attack;

    public AudioClip magicChargingSoundEffect;
    public AudioClip magicAttackSoundEffect;
    public AudioClip physChargingSoundEffect;
    public AudioClip physAttackSoundEffect;


    public void StartMagicCharge()
    {
        magicCharging.Play();
    }

    public void StartMagicChargingSoundEffect()
    {
        if (magicChargingSoundEffect != null)
        {
            charging.clip = magicChargingSoundEffect;
            charging.Play();
        }
    }

    public void StartMagicAttack()
    {
        magicAttack.Play();
    }

    public void StartMagicAttackSoundEffect()
    {
        if (magicAttackSoundEffect != null)
        {
            attack.clip = magicAttackSoundEffect;
            attack.Play();
        }
    }

    public void StartPhysCharging()
    {
        if (physCharging != null) physCharging.Play();
    }

    public void StartPhysChargingSoundEffect()
    {
        if (physChargingSoundEffect != null)
        {
            charging.clip = physChargingSoundEffect;
            charging.Play();
        }
    }

    public void StartPhysAttack()
    {
        physAttack.Play();
    }

    public void StartPhysAttackSoundEffect()
    {
        if (physAttackSoundEffect != null)
        {
            attack.clip = physAttackSoundEffect;
            attack.Play();
        }
    }

    public void StopChargingEffect()
    {
        charging.Stop();
    }

    public void StopAttackEffect()
    {
        attack.Stop();
    }
    
}
