using UnityEngine;

public class EnemiesPlaygroundmanager : MonoBehaviour
{
    [Header("Strings")]
    private string _enemyTag;
    private string _enemyProjectileTag;
    private string _playerProjectileTag;

    private void Awake()
    {
        EnemiesPlaygroundInitialization();
    }

    private void EnemiesPlaygroundInitialization()
    {
        _enemyTag = "Enemy";
        _enemyProjectileTag = "EnemyProjectile";
        _playerProjectileTag = "PlayerProjectile";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_enemyTag))
            collision.gameObject.GetComponent<Enemy2Controller>().OnOutOfBoundAndPlayerCollision();
        else if (collision.gameObject.CompareTag(_enemyProjectileTag))
            collision.gameObject.GetComponent<EnemyProjectileController>().OnOutOfBoundAndPlayerCollision();
        else if (collision.gameObject.CompareTag(_playerProjectileTag))
            collision.gameObject.GetComponent<PlayerProjectileController>().OnOutOfBoundAndEnemyCollision();
    }
}
