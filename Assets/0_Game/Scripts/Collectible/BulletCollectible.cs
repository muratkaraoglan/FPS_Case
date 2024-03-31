using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCollectible : MonoBehaviour
{
    [SerializeField] private Vector2Int _bulletDropRange;
    [SerializeField] private Vector2 _spawnTimeIntervalRange;
    [SerializeField] private ParticleSystem _ammoParticle;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private GameObject _worldCanvas;
    private int _spawnedAmmoCount;
    private float _respawnTime;

    private void OnEnable()
    {
        Init();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out FPSController fPSController))
        {
            fPSController.BaseGunController.TryFillMagazine(ref _spawnedAmmoCount);
            _ammoCountText.SetText(_spawnedAmmoCount.ToString());
            if (_spawnedAmmoCount == 0)
            {
                StartCoroutine(Respawn());
            }
        }
    }

    void Init()
    {
        _spawnedAmmoCount = Random.Range(_bulletDropRange.x, _bulletDropRange.y);
        _respawnTime = Random.Range(_spawnTimeIntervalRange.x, _spawnTimeIntervalRange.y);
        _ammoCountText.SetText(_spawnedAmmoCount.ToString());
        _worldCanvas.SetActive(true);
        _collider.enabled = true;
        float areaBoundx = GameManager.Instance.AreaBound.x;
        float areaBoundz = GameManager.Instance.AreaBound.y;

        transform.position = new Vector3(Random.Range(-areaBoundx, areaBoundx), 0, Random.Range(-areaBoundz, areaBoundz));
        _ammoParticle.Play();
    }

    IEnumerator Respawn()
    {
        _ammoParticle.Stop();
        _collider.enabled = false;
        _worldCanvas.SetActive(false);
        yield return new WaitForSeconds(_respawnTime);
        Init();
    }

}
