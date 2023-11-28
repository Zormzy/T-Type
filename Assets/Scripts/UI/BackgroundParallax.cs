using UnityEngine;

public class BackgroundParalax : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _backgroundSpriteRenderer;

    [Header("Variables")]
    public float _backgroundSpeed;

    private void Awake()
    {
        BackgroundParallaxInitialization();
    }

    private void Update()
    {
        _backgroundSpriteRenderer.material.mainTextureOffset -= new Vector2(0f, _backgroundSpeed * Time.deltaTime);
    }

    private void BackgroundParallaxInitialization()
    {
        _backgroundSpeed = -0.1f;
    }
}
