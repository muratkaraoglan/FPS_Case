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
    [SerializeField] protected LayerMask aimIgnoreLayerMask;
    protected float nextFireTime;
    protected Camera fpsCamera;
    public abstract void Fire();

    public void SetCamera(Camera camera) => fpsCamera = camera;
}
