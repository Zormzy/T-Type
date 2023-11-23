using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _enemyProjectileRigidBody;
    private EnemiesProjectilesStack _enemyProjectilesStack;
    private Transform _enemyProjectileTransform;

    [Header("Variables")]
    public Vector2 _enemyProjectileDirectionVector2;
    public float _enemyProjectileSpeed;

    private void Awake()
    {
        EnemyProjectileControllerInitialization();
    }

    public void OnFireAction()
    {
        _enemyProjectileDirectionVector2 = _enemyProjectileTransform.TransformDirection(Vector3.up);
        _enemyProjectileRigidBody.velocity = _enemyProjectileDirectionVector2 * _enemyProjectileSpeed;
    }

    public void OnOutOfBoundAndPlayerCollision()
    {
        _enemyProjectileRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _enemyProjectilesStack._enemyProjectilesStack.Push(gameObject);
    }

    private void EnemyProjectileControllerInitialization()
    {
        _enemyProjectileTransform = GetComponent<Transform>();
        _enemyProjectilesStack = GameObject.Find("EnemiesProjectiles").GetComponent<EnemiesProjectilesStack>();
        _enemyProjectileSpeed = 1.5f;
    }
}
