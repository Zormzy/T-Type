using UnityEngine;

public class ExplosionFXController : MonoBehaviour
{
    [Header("Components")]
    private FXStacks _FXStack;
    public GameObject _actualExplosion;

    private void Awake()
    {
        ExplosionFXControllerInitialization();
    }

    public void OnExplosionAnimationEnd()
    {
        _actualExplosion.SetActive(false);
        _FXStack._explosionFXStack.Push(_actualExplosion);
    }

    private void ExplosionFXControllerInitialization()
    {
        _FXStack = GameObject.Find("FX").GetComponent<FXStacks>();
        _actualExplosion = this.gameObject;
    }
}
