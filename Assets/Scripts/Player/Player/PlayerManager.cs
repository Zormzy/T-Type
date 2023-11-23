using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _playerRigidBody;
    private PlayerController _playerController;

    [Header("Variables")]
    private string _enemyCollisionTag;
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
        _enemyCollisionTag = "Enemy";
        _enemyProjectileCollisionTag = "EnemyProjectile";
        _playerIsAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_collisionTag == _enemyCollisionTag || _collisionTag == _enemyProjectileCollisionTag)
            _playerController.EnemyCollision();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    _collisionTag = collision.gameObject.tag;

    //    if (_collisionTag == _enemyCollisionTag || _collisionTag == _enemyProjectileCollisionTag)
    //        _playerController.EnemyCollision();
    //}
}
