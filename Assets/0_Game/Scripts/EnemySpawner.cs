using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField, Min(5)] private int _initialNumberOfEnemies;
    [SerializeField, Min(0)] private int _newEnemySpawnCount;
    [SerializeField, Min(0),Tooltip("How many levels to spawn a new enemy")] private int _levelCountToSpawnNewEnemy;
    [SerializeField] private Vector2 _timeIntervalForRespawnEnemy;

    private WaitForSeconds _waitOneSecond;
    private void OnEnable()
    {
        _waitOneSecond = new WaitForSeconds(1);
        ExperienceSystem.Instance.OnLevelUp += OnLevelUp;
        for (int i = 0; i < _initialNumberOfEnemies; i++)
        {
            Instantiate(_enemyPrefab);
        }
    }

    private void OnLevelUp()
    {
        if (_levelCountToSpawnNewEnemy > 0)
        {
            if (ExperienceSystem.Instance.CurrentLevel % _levelCountToSpawnNewEnemy != 0) return;

            for (int i = 0; i < _newEnemySpawnCount; i++)
            {
                Instantiate(_enemyPrefab);
            }
        }
    }

    private void OnDisable()
    {
        ExperienceSystem.Instance.OnLevelUp -= OnLevelUp;
    }

    public void RespawnEnemy(GameObject enemy)
    {
        StartCoroutine(Respawner(enemy));
    }

    IEnumerator Respawner(GameObject enemy)
    {
        yield return _waitOneSecond;
        enemy.SetActive(false);
        float respawnDelay = Random.Range(_timeIntervalForRespawnEnemy.x, _timeIntervalForRespawnEnemy.y);
        yield return new WaitForSeconds(respawnDelay);
        enemy.SetActive(true);
    }

}
