using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _playerProjectileRigidBody;
    [SerializeField] private Transform _playerProjectileTransform;
    public PlayerFireController _playerFireController;

    [Header("Variables")]
    private Vector2 _playerProjectileDirectionVector2;
    public float _playerProjectileSpeed;

    private void Awake()
    {
        PlayerProjectileControllerInitialization();
    }

    public void OnFireAction()
    {
        _playerProjectileRigidBody.velocity = _playerProjectileDirectionVector2 * _playerProjectileSpeed;
    }

    public void OnOutOfBoundAndEnemyCollision()
    {
        _playerProjectileRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _playerFireController._playerProjectileStack.Push(gameObject);
    }

    private void PlayerProjectileControllerInitialization()
    {
        _playerProjectileDirectionVector2 = Vector2.up;
        _playerProjectileSpeed = 5f;
    }
}
