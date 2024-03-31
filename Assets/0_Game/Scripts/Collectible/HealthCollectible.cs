using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private Vector2Int _healRange;
    [SerializeField] private Vector2 _spawnTimeIntervalRange;
    [SerializeField] private ParticleSystem _healParticle;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private TextMeshProUGUI _healAmountText;
    [SerializeField] private GameObject _worldCanvas;

    private int _healAmount;
    private float _respawnTime;
    private void OnEnable()
    {
        Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FPSController controller))
        {
            controller.TryHeal(_healAmount);
            StartCoroutine(Respawn());
        }
    }


    void Init()
    {
        _healAmount = Random.Range(_healRange.x, _healRange.y);
        _respawnTime = Random.Range(_spawnTimeIntervalRange.x, _spawnTimeIntervalRange.y);
        _healAmountText.SetText(_healAmount.ToString());
        _worldCanvas.SetActive(true);
        _collider.enabled = true;
        float areaBoundx = GameManager.Instance.AreaBound.x;
        float areaBoundz = GameManager.Instance.AreaBound.y;

        transform.position = new Vector3(Random.Range(-areaBoundx, areaBoundx), 0, Random.Range(-areaBoundz, areaBoundz));
        _healParticle.Play();
    }

    IEnumerator Respawn()
    {
        _healParticle.Stop();
        _collider.enabled = false;
        _worldCanvas.SetActive(false);
        yield return new WaitForSeconds(_respawnTime);
        Init();
    }
}
