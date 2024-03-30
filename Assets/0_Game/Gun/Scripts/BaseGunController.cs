using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGunController : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected int magazineCapacity;
    [SerializeField] protected float bulletDamage;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float fireDelay;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected ParticleSystem muzzleParticle;
    protected int currentCapacity;
    protected float nextFireTime;
    public abstract void Fire();
}
