using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP5Controller : BaseGunController
{
    public override void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireDelay;
            animator.Play(StringHelper.FIRE, -1, 0);
        }
    }
}
