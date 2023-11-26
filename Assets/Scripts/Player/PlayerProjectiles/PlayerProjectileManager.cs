using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _playerProjectileSpriteRenderer;
    [SerializeField] private Rigidbody2D _playerProjectileRigidBody;
    [SerializeField] private PlayerProjectileController _playerProjectileController;

    [Header("Variables")]
    private string _enemyN1CollisionTag;
    private string _enemyN2CollisionTag;
    private string _collisionTag;
    public int _damage;

    private void Awake()
    {
        PlayerManagerInitialization();
    }

    private void PlayerManagerInitialization()
    {
        _enemyN1CollisionTag = "EnemyN1";
        _enemyN2CollisionTag = "EnemyN2";
        _damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_collisionTag == _enemyN1CollisionTag || _collisionTag == _enemyN2CollisionTag)
            _playerProjectileController.OnOutOfBoundAndEnemyCollision();
    }
}
