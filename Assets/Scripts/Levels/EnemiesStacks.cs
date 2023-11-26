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
        EnemiesStackVariablesInitialization();
        EnemiesStackInitialization();
    }

    private void EnemiesStackInitialization()
    {
        for (int i = 0; i < _enemyNormal1Count; i++)
        {
            GameObject _enemy = Instantiate(_enemyNormal1Prefab);
            _enemy.SetActive(false);
            _enemy.transform.parent = _enemyTransformParent;
            _enemy.name = _enemyNormal1Name + i.ToString();
            _enemyNormal1Stack.Push(_enemy);
        }

        for (int i = 0; i < _enemyNormal2Count; i++)
        {
            GameObject _enemy = Instantiate(_enemyNormal2Prefab);
            _enemy.SetActive(false);
            _enemy.transform.parent = _enemyTransformParent;
            _enemy.name = _enemyNormal2Name + i.ToString();
            _enemyNormal2Stack.Push(_enemy);
        }
    }

    private void EnemiesStackVariablesInitialization()
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
