using UnityEngine;

public class EnemiesPlaygroundmanager : MonoBehaviour
{
    private string _enemyTag;
    private string _enemyProjectileTag;

    private void Awake()
    {
        EnemiesPlaygroundInitialization();
    }

    private void EnemiesPlaygroundInitialization()
    {
        _enemyTag = "Enemy";
        _enemyProjectileTag = "EnemyProjectile";
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_enemyTag))
            collision.gameObject.GetComponent<EnemyProjectileController>().OnOutOfBoundAndPlayerCollision();
        else if (collision.gameObject.CompareTag(_enemyProjectileTag))
            collision.gameObject.GetComponent<Enemy2Controller>().OnOutOfBoundAndPlayerCollision();
    }
}
