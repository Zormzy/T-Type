using UnityEngine;

public class EnemiesGeneration : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyTransformParent;
    private EnemiesStacks _enemyStacks;
    private GameObject _enemyToLaunch;

    [Header("Enemies infos")]
    [SerializeField] private Transform _enemySpawnPoint;
    private Vector3 _enemySpawnPointRand;
    private int _enemyToSpawn;

    [Header("Enemies spawn timer")]
    public float _enemySpawnTimer;
    public float _enemySpawnTimerCounter;
    private bool _enemyAsSpawn;

    private void Awake()
    {
        EnemiesGenerationInitialization();
    }

    private void Update()
    {
        EnemiesSpawnTimer();
    }

    private void EnemiesSpawnTimer()
    {
        if (_enemySpawnTimerCounter >= _enemySpawnTimer)
        {
            _enemySpawnPointRand.Set(_enemySpawnPoint.position.x + Random.Range(-5f, 5f), _enemySpawnPoint.position.y, _enemySpawnPoint.position.z);
            _enemyToSpawn = Random.Range(0, 2);
            EnemySpawn();
            _enemySpawnTimer = Random.Range(4.00f, 6.00f);
            _enemySpawnTimerCounter = 0f;
            _enemyAsSpawn = false;
        }
        else
            _enemySpawnTimerCounter += Time.deltaTime;
    }

    private void EnemySpawn()
    {
        switch (_enemyToSpawn)
        {
            case 0:
                _enemyToLaunch = _enemyStacks._enemyNormal1Stack.Pop();
                break;
            case 1:
                _enemyToLaunch = _enemyStacks._enemyNormal2Stack.Pop();
                break;
            default:
                break;
        }
        
        _enemyToLaunch.GetComponent<EnemiesManager>().EnemyReset();
        _enemyToLaunch.GetComponent<EnemiesManager>()._movementTimer = Random.Range(0f, 30f);
        _enemyToLaunch.SetActive(true);
        _enemyToLaunch.transform.position = _enemySpawnPointRand;
        _enemyAsSpawn = true;
    }

    private void EnemiesGenerationInitialization()
    {
        _enemyStacks = GameObject.Find("Enemies").GetComponent<EnemiesStacks>();
        _enemySpawnTimer = 5f;
        _enemySpawnTimerCounter = 2f;
        _enemyAsSpawn = false;
    }
}
