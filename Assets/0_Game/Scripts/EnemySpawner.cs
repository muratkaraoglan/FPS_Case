using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    private void OnEnable()
    {
        ExperienceSystem.Instance.OnLevelUp += OnLevelUp;
    }

    private void OnLevelUp()
    {
        
    }

    private void OnDisable()
    {
        ExperienceSystem.Instance.OnLevelUp -= OnLevelUp;
    }
}
