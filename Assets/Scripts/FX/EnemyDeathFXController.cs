using UnityEngine;

public class EnemyDeathFXController : MonoBehaviour
{
    [Header("Components")]
    private FXStacks _FXStack;
    public GameObject _actualExplosion;

    private void Awake()
    {
        EnemyDeathFXControllerInitialization();
    }

    public void OnExplosionAnimationEnd()
    {
        _actualExplosion.SetActive(false);
        _FXStack._enemyDeathFXStack.Push(_actualExplosion);
    }

    private void EnemyDeathFXControllerInitialization()
    {
        _FXStack = GameObject.Find("FX").GetComponent<FXStacks>();
        _actualExplosion = this.gameObject;
    }
}
