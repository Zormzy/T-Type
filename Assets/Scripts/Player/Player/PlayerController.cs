using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRigidbody;

    public void EnemyCollision()
    {
        _playerRigidbody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
