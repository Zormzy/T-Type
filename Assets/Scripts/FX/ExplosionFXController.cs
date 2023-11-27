using UnityEngine;

public class ExplosionFXController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _explosion;
    private FXStacks _explosionFXStack;
    public GameObject _actualExplosion;

    private void Awake()
    {
        ExplosionFXControllerInitialization();
    }

    public void OnExplosionAnimationEnd()
    {
        _actualExplosion.SetActive(false);
        _explosionFXStack._explosionFXStack.Push(_actualExplosion);
    }

    private void ExplosionFXControllerInitialization()
    {
        _explosionFXStack = GameObject.Find("FX").GetComponent<FXStacks>();
        _actualExplosion = this.gameObject;
    }
}
