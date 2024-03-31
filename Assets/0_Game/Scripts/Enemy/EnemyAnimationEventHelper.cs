using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventHelper : MonoBehaviour
{
    [SerializeField] private GameObject _damageApplierGO;

    public void EnableDamageApplier()
    {
        _damageApplierGO.SetActive(true);
    }
    public void DisableDamageApplier()
    {
        _damageApplierGO.SetActive(false);
    }
}
