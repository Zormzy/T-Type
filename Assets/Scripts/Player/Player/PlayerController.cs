using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private Material _playerHitFlashMaterial;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    private Material _playerOriginMaterial;
    private Coroutine _playerHitFlashRoutine;
    private float _playerHitFlashDuration = 0.1f;

    private void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerOriginMaterial = _playerSpriteRenderer.material;
    }

    public void EnemyCollision()
    {
        Flash();
    }

    public void PlayerDeath()
    {
        _playerRigidbody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void Flash()
    {
        if (_playerHitFlashRoutine != null)
            StopCoroutine(_playerHitFlashRoutine);

        _playerHitFlashRoutine = StartCoroutine(PlayerHitFlashRoutine());
    }

    private IEnumerator PlayerHitFlashRoutine()
    {
        _playerSpriteRenderer.material = _playerHitFlashMaterial;
        yield return new WaitForSeconds(_playerHitFlashDuration);
        _playerSpriteRenderer.material = _playerOriginMaterial;
        _playerHitFlashRoutine = null;
    }
}
