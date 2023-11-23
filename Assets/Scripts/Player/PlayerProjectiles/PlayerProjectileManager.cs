using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _playerProjectileSpriteRenderer;
    [SerializeField] private Rigidbody2D _playerProjectileRigidBody;
    [SerializeField] private PlayerProjectileController _playerProjectileController;

    [Header("Variables")]
    private string _collisionTag;

    private void Awake()
    {
        PlayerManagerInitialization();
    }

    private void PlayerManagerInitialization()
    {
        _collisionTag = "Enemy";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_playerProjectileRigidBody.CompareTag(_collisionTag))
        {
            _playerProjectileController.OnOutOfBoundAndEnemyCollision();
            collision.gameObject.GetComponent<EnemiesManager>().OnPlayerProjectileCollision();
        }
    }
}
