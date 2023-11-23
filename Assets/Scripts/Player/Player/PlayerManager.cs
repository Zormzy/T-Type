using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _playerRigidBody;
    private PlayerController _playerController;

    [Header("Variables")]
    private string _enemyN1CollisionTag;
    private string _enemyN2CollisionTag;
    private string _enemyProjectileCollisionTag;
    private string _collisionTag;
    private bool _playerIsAlive;

    private void Awake()
    {
        PlayerManagerInitialization();
    }

    private void PlayerManagerInitialization()
    {
        _playerController = GetComponent<PlayerController>();
        _enemyN1CollisionTag = "EnemyN1";
        _enemyN2CollisionTag = "EnemyN2";
        _enemyProjectileCollisionTag = "EnemyProjectile";
        _playerIsAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_collisionTag == _enemyN1CollisionTag || _collisionTag == _enemyN2CollisionTag || _collisionTag == _enemyProjectileCollisionTag)
            _playerController.EnemyCollision();
    }
}
