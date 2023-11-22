using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _playerRigidBody;

    [Header("Variables")]
    private string _enemyCollisionTag;
    private string _enemyProjectileCollisionTag;
    private bool _playerIsAlive;

    private void Awake()
    {
        PlayerManagerInitialization();
    }

    private void PlayerManagerInitialization()
    {
        _enemyCollisionTag = "Enemy";
        _enemyProjectileCollisionTag = "EnemyProjectile";
        _playerIsAlive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _enemyProjectileCollisionTag = collision.gameObject.tag;

        if (_playerRigidBody.CompareTag(_enemyCollisionTag) || _playerRigidBody.CompareTag(_enemyProjectileCollisionTag))
            _playerIsAlive = false;
    }
}
