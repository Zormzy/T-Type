using UnityEngine;

public class PlayerGeneration : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _playerPrefab;

    [Header("Player infos")]
    [SerializeField] private Transform _playerSpawnPoint;

    private void Awake()
    {
        GameObject _player = Instantiate(_playerPrefab);
        _player.transform.position = _playerSpawnPoint.position;
        _player.name = "Player";
    }
}
