using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneration : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyTransformParent;

    [Header("Enemies infos")]
    [SerializeField] private Transform _enemySpawnPoint;
    public List<GameObject> Enemies;
    public int _enemiesCount;

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
            EnenmySpawn();
            _enemySpawnTimerCounter = 0f;
            _enemyAsSpawn = false;
        }
        else
            _enemySpawnTimerCounter += Time.deltaTime;
    }

    private void EnenmySpawn()
    {
        foreach (GameObject enemy in Enemies)
        {
            if (!enemy.activeSelf && !_enemyAsSpawn)
            {
                enemy.SetActive(true);
                enemy.transform.position = _enemySpawnPoint.position;
                _enemyAsSpawn = true;
            }
        }
    }

    private void EnemiesStartSpawn()
    {
        for (int i = 0; i < _enemiesCount; i++)
        {
            GameObject _enemy = Instantiate(_enemyPrefab);
            _enemy.SetActive(false);
            _enemy.transform.parent = _enemyTransformParent;
            _enemy.transform.position = _enemySpawnPoint.position;
            _enemy.name = "Enemy n°" + i.ToString();
            Enemies.Add(_enemy);
        }
    }

    private void EnemiesGenerationInitialization()
    {
        Enemies = new List<GameObject>();
        _enemiesCount = 10;
        EnemiesStartSpawn();
        _enemySpawnTimer = 5f;
        _enemySpawnTimerCounter = 0f;
        _enemyAsSpawn = false;
    }
}
