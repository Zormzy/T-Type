using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectilesStack : MonoBehaviour
{
    [SerializeField] GameObject _enemyProjectilePrefab;
    private Transform _enemyProjectileTransformParent;
    public Stack<GameObject> _enemyProjectilesStack;
    private string _playerProjectileName;
    public int _enemyProjectilesCount;

    private void Awake()
    {
        EnemiesProjectilesStackVariablesInitialization();
        EnemiesProjectilesStackInitialization();
    }

    private void EnemiesProjectilesStackInitialization()
    {
        for (int i = 0; i < _enemyProjectilesCount; i++)
        {
            GameObject _projectile = Instantiate(_enemyProjectilePrefab);
            _projectile.SetActive(false);
            _projectile.transform.parent = _enemyProjectileTransformParent;
            _projectile.name = _playerProjectileName + i.ToString();
            _enemyProjectilesStack.Push(_projectile);
        }
    }

    private void EnemiesProjectilesStackVariablesInitialization()
    {
        _enemyProjectileTransformParent = this.transform;
        _enemyProjectilesStack = new Stack<GameObject>();
        _enemyProjectilesCount = 1000;
        _playerProjectileName = "Enemy Projectile n°";
    }
}
