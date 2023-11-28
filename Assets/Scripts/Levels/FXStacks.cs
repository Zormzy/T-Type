using System.Collections.Generic;
using UnityEngine;

public class FXStacks : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _explosionFXPrefab;
    [SerializeField] private GameObject _enemyDeathFXPrefab;
    private Transform _explosionFXTransformParent;
    private Transform _enemyDeathFXTransformParent;

    [Header("Variables")]
    public Stack<GameObject> _explosionFXStack;
    public Stack<GameObject> _enemyDeathFXStack;
    private int _explosionFXCount;
    private int _enemyDeathFXCount;
    private string _explosionFXName;
    private string _enemyDeathFXName;

    private void Awake()
    {
        FXStackInitialization();
    }

    private void ExplosionFXStackInitialization()
    {
        for (int i = 0; i < _explosionFXCount; i++)
        {
            GameObject _explosionFX = Instantiate(_explosionFXPrefab);
            _explosionFX.SetActive(false);
            _explosionFX.transform.parent = _explosionFXTransformParent;
            _explosionFX.name = _explosionFXName + i.ToString();
            _explosionFXStack.Push(_explosionFX);
        }
    }

    private void EnemiesDeathFXStackInitialization()
    {
        for (int i = 0; i < _enemyDeathFXCount; i++)
        {
            GameObject _enemyDeathFX = Instantiate(_enemyDeathFXPrefab);
            _enemyDeathFX.SetActive(false);
            _enemyDeathFX.transform.parent = _enemyDeathFXTransformParent;
            _enemyDeathFX.name = _enemyDeathFXName + i.ToString();
            _enemyDeathFXStack.Push(_enemyDeathFX);
        }
    }

    private void FXStackInitialization()
    {
        _explosionFXTransformParent = GameObject.Find("ExplosionsFX").transform;
        _explosionFXName = "Explosion fx n°";
        _explosionFXCount = 30;
        _explosionFXStack = new Stack<GameObject>();
        ExplosionFXStackInitialization();

        _enemyDeathFXTransformParent = GameObject.Find("EnemiesDeathFX").transform;
        _enemyDeathFXName = "Enemy death fx n°";
        _enemyDeathFXCount = 5;
        _enemyDeathFXStack = new Stack<GameObject>();
        EnemiesDeathFXStackInitialization();
    }
}
