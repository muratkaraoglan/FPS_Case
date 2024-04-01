using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class BulletCollectible : MonoBehaviour
{
    [SerializeField] private Vector2Int _bulletDropRange;
    [SerializeField] private ParticleSystem _ammoParticle;
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    private int _spawnedAmmoCount;

    private void Awake()
    {
        ConstraintSource constraintSource = new ConstraintSource()
        {
            sourceTransform = GameManager.Instance.FPSController.CameraTransform,
            weight = 1.0f,
        };
        _lookAtConstraint.SetSource(0, constraintSource);
    }

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
                CollectibleSpawner.Instance.Respawn(gameObject, CollectibleType.Ammo);
            }
        }
    }

    void Init()
    {
        _spawnedAmmoCount = Random.Range(_bulletDropRange.x, _bulletDropRange.y);
        _ammoCountText.SetText(_spawnedAmmoCount.ToString());
        float areaBoundx = GameManager.Instance.AreaBound.x;
        float areaBoundz = GameManager.Instance.AreaBound.y;
        transform.position = new Vector3(Random.Range(-areaBoundx, areaBoundx), 0, Random.Range(-areaBoundz, areaBoundz));
        _ammoParticle.Play();
    }
}
