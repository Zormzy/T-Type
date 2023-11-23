using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _playerRigidBody;

    [Header("Movement")]
    public Vector2 _playerDirectionVector2;
    public float _playerSpeed;

    private void Awake()
    {
        PlayerInputsControllerInitialize();
    }

    public void OnPlayerMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerDirectionVector2 = context.ReadValue<Vector2>();
            _playerRigidBody.velocity = _playerDirectionVector2 * _playerSpeed;
        }
        else
            _playerRigidBody.velocity = Vector2.zero;
    }

    private void PlayerInputsControllerInitialize()
    {
        _playerDirectionVector2 = Vector2.zero;
        _playerSpeed = 7.5f;
    }
}
