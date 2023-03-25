using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{

    public ParticleSystem magicCharging;
    public ParticleSystem magicAttack;
    public ParticleSystem physCharging;
    public ParticleSystem physAttack;


    public void StartMagicCharge()
    {
        magicCharging.Play();
    }

    public void StartMagicAttack()
    {
        magicAttack.Play();
    }

    public void StartPhysCharging()
    {
        if (physCharging != null) physCharging.Play();
    }

    public void StartPhysAttack()
    {
        physAttack.Play();
    }
    
}
