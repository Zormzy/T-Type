using UnityEngine;

public class EnemiesPlaygroundmanager : MonoBehaviour
{
    [Header("Strings")]
    private string _colliderGO;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.activeSelf)
        {
            _colliderGO = collision.gameObject.tag;

            switch (_colliderGO)
            {
                case "EnemyN1":
                    collision.gameObject.GetComponent<Enemy1Controller>().OnOutOfBoundAndPlayerCollision();
                    break;
                case "EnemyN2":
                    collision.gameObject.GetComponent<Enemy2Controller>().OnOutOfBoundAndPlayerCollision();
                    break;
                case "EnemyProjectile":
                    collision.gameObject.GetComponent<EnemyProjectileController>().OnOutOfBoundAndPlayerCollision();
                    break;
                case "PlayerProjectile":
                    collision.gameObject.GetComponent<PlayerProjectileController>().OnOutOfBoundAndEnemyCollision();
                    break;
                default:
                    break;
            }
        }
    }
}
