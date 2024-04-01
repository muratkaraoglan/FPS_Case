using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private Vector2Int _healRange;
    [SerializeField] private ParticleSystem _healParticle;
    [SerializeField] private TextMeshProUGUI _healAmountText;
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    private int _healAmount;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FPSController controller))
        {
            if (controller.TryHeal(_healAmount))
                CollectibleSpawner.Instance.Respawn(gameObject, CollectibleType.Health);
        }
    }


    void Init()
    {
        _healAmount = Random.Range(_healRange.x, _healRange.y);
        _healAmountText.SetText(_healAmount.ToString());
        float areaBoundx = GameManager.Instance.AreaBound.x;
        float areaBoundz = GameManager.Instance.AreaBound.y;
        transform.position = new Vector3(Random.Range(-areaBoundx, areaBoundx), 0, Random.Range(-areaBoundz, areaBoundz));
        _healParticle.Play();
    }
}
