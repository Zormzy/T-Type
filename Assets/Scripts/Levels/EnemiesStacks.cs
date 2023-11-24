using System.Collections.Generic;
using UnityEngine;

public class EnemiesStacks : MonoBehaviour
{
    [SerializeField] GameObject _enemyNormal1Prefab;
    [SerializeField] GameObject _enemyNormal2Prefab;
    private Transform _enemyTransformParent;
    public Stack<GameObject> _enemyNormal1Stack;
    public Stack<GameObject> _enemyNormal2Stack;
    private string _enemyNormal1Name;
    private string _enemyNormal2Name;
    public int _enemyNormal1Count;
    public int _enemyNormal2Count;

    private void Awake()
    {
        EnemiesProjectilesStackVariablesInitialization();
        EnemiesProjectilesStackInitialization();
    }

    private void EnemiesProjectilesStackInitialization()
    {
        for (int i = 0; i < _enemyNormal1Count; i++)
        {
            GameObject _projectile = Instantiate(_enemyNormal1Prefab);
            _projectile.SetActive(false);
            _projectile.transform.parent = _enemyTransformParent;
            _projectile.name = _enemyNormal1Name + i.ToString();
            _enemyNormal1Stack.Push(_projectile);
        }

        for (int i = 0; i < _enemyNormal2Count; i++)
        {
            GameObject _projectile = Instantiate(_enemyNormal2Prefab);
            _projectile.SetActive(false);
            _projectile.transform.parent = _enemyTransformParent;
            _projectile.name = _enemyNormal2Name + i.ToString();
            _enemyNormal2Stack.Push(_projectile);
        }
    }

    private void EnemiesProjectilesStackVariablesInitialization()
    {
        _enemyTransformParent = this.transform;
        _enemyNormal1Stack = new Stack<GameObject>();
        _enemyNormal2Stack = new Stack<GameObject>();
        _enemyNormal1Count = 20;
        _enemyNormal2Count = 20;
        _enemyNormal1Name = "Enemy normal 1 n°";
        _enemyNormal2Name = "Enemy normal 2 n°";
    }
}
