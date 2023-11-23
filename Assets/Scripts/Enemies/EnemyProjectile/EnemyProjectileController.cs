using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _enemyProjectileRigidBody;
    private EnemiesProjectilesStack _enemyProjectilesStack;
    private Transform _enemyProjectileTransform;

    [Header("Variables")]
    public Vector2 _enemyProjectileDirectionVector2;
    public float _enemyInitialProjectileSpeed;
    public float _enemyMinimalProjectileSpeed;
    public float _enemyProjectileSpeedLerp;
    public int _damage;
    private bool _isLaunched;

    private void Awake()
    {
        EnemyProjectileControllerInitialization();
    }

    private void LateUpdate()
    {
        if (_isLaunched)
        {
            //_enemyCurrentProjectileSpeed = Lerp
            _enemyProjectileRigidBody.velocity = _enemyProjectileDirectionVector2 * Mathf.Lerp(_enemyInitialProjectileSpeed, _enemyMinimalProjectileSpeed, _enemyProjectileSpeedLerp);
        }
    }

    public void OnFireAction()
    {
        _enemyProjectileDirectionVector2 = _enemyProjectileTransform.TransformDirection(Vector3.up);
        _enemyProjectileRigidBody.velocity = _enemyProjectileDirectionVector2 * _enemyInitialProjectileSpeed;
        _isLaunched = true;
    }

    public void OnOutOfBoundAndPlayerCollision()
    {
        _enemyProjectileRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _isLaunched = false;
        _enemyProjectilesStack._enemyProjectilesStack.Push(gameObject);
    }

    private void EnemyProjectileControllerInitialization()
    {
        _enemyProjectileTransform = GetComponent<Transform>();
        _enemyProjectilesStack = GameObject.Find("EnemiesProjectiles").GetComponent<EnemiesProjectilesStack>();
        _enemyInitialProjectileSpeed = 4f;
        _enemyMinimalProjectileSpeed = 0.5f;
        _enemyProjectileSpeedLerp = 0.5f;
        _isLaunched = false;
    }
}
