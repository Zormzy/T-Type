using System.Collections.Generic;
using UnityEngine;

public class FXStacks : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _explosionFXPrefab;
    private Transform _explosionFXTransformParent;

    [Header("Variables")]
    public Stack<GameObject> _explosionFXStack;
    private int _explosionFXCount;
    private string _explosionFXName;

    private void Awake()
    {
        FXStackInitialization();
    }

    private void ExplosionFXStackInitialization()
    {
        for (int i = 0; i < _explosionFXCount; i++)
        {
            GameObject _projectile = Instantiate(_explosionFXPrefab);
            _projectile.SetActive(false);
            _projectile.transform.parent = _explosionFXTransformParent;
            _projectile.name = _explosionFXName + i.ToString();
            _explosionFXStack.Push(_projectile);
        }
    }

    private void FXStackInitialization()
    {
        _explosionFXTransformParent = GameObject.Find("ExplosionsFX").transform;
        _explosionFXName = "Explosion fx n°";
        _explosionFXCount = 10;
        _explosionFXStack = new Stack<GameObject>();
        ExplosionFXStackInitialization();
    }
}
