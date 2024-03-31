using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageApplier : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_stateMachine.Damage);
        }
    }
}
