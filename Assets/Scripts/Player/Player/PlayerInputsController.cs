using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _playerRigidBody;
    [SerializeField] private Animator _playerAnimator;
    private PauseController _pausedController;
    private VictoryController _victoryController;
    private Camera _camera;

    [Header("Movement")]
    private Vector2 _screenBounds;
    private float _playerWidth;
    private float _playerHeight;
    public Vector2 _playerDirectionVector2;
    public float _playerSpeed;


    private void Awake()
    {
        PlayerInputsControllerInitialize();
    }

    private void LateUpdate()
    {
        Vector3 viewpos = transform.position;
        viewpos.x = Mathf.Clamp(viewpos.x, _screenBounds.x * -1 - _playerWidth, _screenBounds.x + _playerWidth);
        viewpos.y = Mathf.Clamp(viewpos.y, _screenBounds.y * -1 - _playerHeight, _screenBounds.y + _playerHeight);
        transform.position = viewpos;
    }

    public void OnPlayerMovement(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
        {
            Cursor.visible = false;
            _victoryController._isGamepadControl = true;
        }
        else
        {
            Cursor.visible = true;
            _victoryController._isGamepadControl = false;
        }

        if (context.performed)
        {
            _playerDirectionVector2 = context.ReadValue<Vector2>();
            _playerRigidBody.velocity = _playerDirectionVector2 * _playerSpeed;
            _playerAnimator.SetBool("_isMoving", true);
        }
        else
        {
            _playerRigidBody.velocity = Vector2.zero;
            _playerAnimator.SetBool("_isMoving", false);
        }
    }

    public void OnPlayerPause(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
        {
            Cursor.visible = false;
            _victoryController._isGamepadControl = true;
        }
        else
        {
            Cursor.visible = true;
            _victoryController._isGamepadControl = false;
        }

        if (context.performed)
            _pausedController.PauseGame(false, false);
    }

    private void PlayerInputsControllerInitialize()
    {
        _camera = Camera.main;
        _pausedController = GameObject.Find("UIManager").GetComponent<PauseController>();
        _victoryController = GameObject.Find("UIManager").GetComponent<VictoryController>();
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        _playerWidth = transform.GetComponent<SpriteRenderer>().sprite.bounds.min.x / 15;
        _playerHeight = transform.GetComponent<SpriteRenderer>().sprite.bounds.min.y / 15;
        _playerDirectionVector2 = Vector2.zero;
        _playerSpeed = 5f;
    }
}
