using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRigidbody;
    private int _damage;
    private bool _pierce;


    public void Init(int damage, float speed, bool pierce, Vector3 direction)
    {
        _bulletRigidbody.velocity = Vector3.zero;
        _damage = damage;
        _pierce = pierce;
        _bulletRigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
            if (!_pierce) gameObject.SetActive(false);
        }
    }
}
