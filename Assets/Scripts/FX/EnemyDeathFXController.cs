using UnityEngine;

public class EnemyDeathFXController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _explosion;
    private FXStacks _enemyDeathFXStack;
    public GameObject _actualExplosion;

    private void Awake()
    {
        EnemyDeathFXControllerInitialization();
    }

    public void OnExplosionAnimationEnd()
    {
        _actualExplosion.SetActive(false);
        _enemyDeathFXStack._explosionFXStack.Push(_actualExplosion);
    }

    private void EnemyDeathFXControllerInitialization()
    {
        _enemyDeathFXStack = GameObject.Find("FX").GetComponent<FXStacks>();
        _actualExplosion = this.gameObject;
    }
}
