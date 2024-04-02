using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;

public class EnemyStateMachine : StateMachine, IDamagable
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField, Min(1)] private int _maxHealth;
    [SerializeField] private int _exprienceAmount;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private RotationConstraint _rotationConstraint;
    [field: SerializeField] public int Score { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float MaxAreaRadius { get; private set; }
    [field: SerializeField] public float PlayerDetectRadius { get; private set; }
    [field: SerializeField] public float AttackRadius { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float ChaseSpeed { get; private set; }

    public Vector3 Center { get; private set; }
    private int _currentHealth = 0;
    [HideInInspector] public bool IsDead;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Center, MaxAreaRadius);//Patrol area


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectRadius);//player detect radius

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);//player attack radius
    }

    private void Awake()
    {
        ConstraintSource constraintSource = new ConstraintSource()
        {
            sourceTransform = GameManager.Instance.FPSController.CameraTransform,
            weight = 1.0f,
        };
        _rotationConstraint.SetSource(0, constraintSource);

    }
    private void OnEnable()
    {
        IsDead = false;
        _currentHealth = _maxHealth;
        _healthBarImage.fillAmount = 1f;
        _healthBarImage.transform.parent.gameObject.SetActive(true);
        float areaBoundx = Mathf.Abs(GameManager.Instance.AreaBound.x) * .5f;
        float areaBoundz = Mathf.Abs(GameManager.Instance.AreaBound.y) * .5f;
        NavMesh.SamplePosition(new Vector3(Random.Range(-areaBoundx, areaBoundx), 0, Random.Range(-areaBoundz, areaBoundz)), out NavMeshHit hit, 200, NavMesh.AllAreas);
        transform.position = hit.position;
        Center = transform.position;
        SwitchState(new EnemyIdleState(this));
    }

    public Vector3 GetRandomPointInSafeArae()
    {
        Vector3 normalizedPoint = Random.insideUnitSphere;
        Vector3 worldPoint = normalizedPoint * MaxAreaRadius + Center;
        worldPoint.y = Center.y;
        return worldPoint;
    }

    public Vector3 Position => transform.position;

    public void TakeDamage(int damage)
    {
        if (IsDead) return;
        _currentHealth -= damage;
        float fillAmount = Mathf.InverseLerp(0, _maxHealth, _currentHealth);
        _healthBarImage.fillAmount = fillAmount;
        if (_currentHealth <= 0)
        {
            IsDead = true;
            ExperienceSystem.Instance.GainXP(_exprienceAmount);
            SwitchState(new EnemyDeathState(this));
        }
        if (_currentHealth < 0) damage = _currentHealth + damage;
        UIController.Instance.ShowDamageText(damage);
    }

    public void CloseCanvas()
    {
        _healthBarImage.transform.parent.gameObject.SetActive(false);
    }
}
