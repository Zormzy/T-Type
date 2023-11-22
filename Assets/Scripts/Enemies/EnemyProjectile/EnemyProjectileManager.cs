using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _enemyProjectileSpriteRenderer;
    [SerializeField] private Rigidbody2D _enemyProjectileRigidBody;
    [SerializeField] private EnemyProjectileController _enemyProjectileController;
    //[SerializeField] private Camera _camera;
    //[SerializeField] private GameObject _enemyPlaygroundGO;
    //private Rect _enemyplaygroundRect;

    [Header("Variables")]
    private string _collisionTag;

    private void Awake()
    {
        EnemyManagerInitialization();
    }

    private void Update()
    {
        //if (!_enemyProjectileSpriteRenderer.isVisible)
        //    _enemyProjectileController.OnOutOfBoundAndPlayerCollision();

        //Debug.Log("in camera rect : " + _camera.rect.Contains(gameObject.transform.position));

        //if (_enemyPlayground.)

        //if (!_camera.rect.Contains(gameObject.transform.position))
        //    _enemyProjectileController.OnOutOfBoundAndPlayerCollision();
    }

    private void EnemyManagerInitialization()
    {
        _collisionTag = "Player";
        //_enemyPlaygroundGO = GameObject.Find("EnemiesPlayground");
        //_enemyplaygroundRect = _enemyPlaygroundGO.transform;
        //_camera = Camera.main;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionTag = collision.gameObject.tag;

        if (_enemyProjectileRigidBody.CompareTag(_collisionTag))
        {
            _enemyProjectileController.OnOutOfBoundAndPlayerCollision();
            // collision.gameObject.OnEnemyProjectileCollision();
        }
    }
}
