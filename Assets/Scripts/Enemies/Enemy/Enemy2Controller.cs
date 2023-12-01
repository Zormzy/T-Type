using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _enemyRigidBody;
    [SerializeField] Transform _enemyTransform;
    [SerializeField] private AudioClip _enemyFireAudioClip;
    private AudioSource _audioSource;

    [Header("Projectiles")]
    private List<Quaternion> _enemyProjectileDirectionQuaternionList;
    private EnemiesProjectilesStack _enemyProjectilesStack;
    private EnemiesStacks _enemyStacks;
    private GameObject _enemyProjectileToLaunch;
    public int _enemyProjectilesperAttackCount;
    private int _enemyProjectilesRotationListIndex;

    [Header("Enemies spawn timer")]
    private bool _isFiring;
    private int _pattern1WaveCount;
    private int _pattern1WaveMaxCount;
    public float _enemyProjectileSpawnTimer;
    public float _enemyProjectileSpawnTimerCounter;

    private void Awake()
    {
        EnemyControllerInitialization();
    }

    private void Update()
    {
        EnemyProjectilesSpawnTimer();
    }

    public void EnemyProjectilesSpawnTimer()
    {
        if (_enemyProjectileSpawnTimerCounter >= _enemyProjectileSpawnTimer && !_isFiring)
        {
            if (Random.value <= 0.5f)
                StartCoroutine(Pattern1Timer());
            else
                StartCoroutine(Pattern2Timer());

            _isFiring = true;
        }
        else if (_enemyProjectileSpawnTimerCounter < _enemyProjectileSpawnTimer)
            _enemyProjectileSpawnTimerCounter += Time.deltaTime;
    }

    private void EnemyProjectileSpawnPattern1()
    {
        _audioSource.PlayOneShot(_enemyFireAudioClip, 0.25f);
        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileToLaunch = _enemyProjectilesStack._enemyProjectilesStack.Pop();
            _enemyProjectileToLaunch.SetActive(true);
            _enemyProjectileToLaunch.transform.position = _enemyTransform.position;
            _enemyProjectileToLaunch.transform.rotation = _enemyProjectileDirectionQuaternionList[i];

            if (_enemyProjectileToLaunch.GetComponent<EnemyProjectileController>() != null)
                _enemyProjectileToLaunch.GetComponent<EnemyProjectileController>().OnFireAction();
        }
    }

    private void EnemyProjectileSpawnPattern2()
    {
        _audioSource.PlayOneShot(_enemyFireAudioClip, 0.25f);
        _enemyProjectileToLaunch = _enemyProjectilesStack._enemyProjectilesStack.Pop();
        _enemyProjectileToLaunch.SetActive(true);
        _enemyProjectileToLaunch.transform.position = _enemyTransform.position;
        _enemyProjectileToLaunch.transform.rotation = _enemyProjectileDirectionQuaternionList[_enemyProjectilesRotationListIndex - 1];

        if (_enemyProjectileToLaunch.GetComponent<EnemyProjectileController>() != null)
            _enemyProjectileToLaunch.GetComponent<EnemyProjectileController>().OnFireAction();
    }

    public void OnOutOfBoundAndPlayerCollision()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _enemyStacks._enemyNormal2Stack.Push(gameObject);
    }

    IEnumerator Pattern1Timer()
    {
        if (_pattern1WaveCount > 0)
        {
            EnemyProjectileSpawnPattern1();
            _pattern1WaveCount--;
            yield return new WaitForSeconds(0.15f);
            StartCoroutine(Pattern1Timer());
        }
        else
        {
            _pattern1WaveCount = _pattern1WaveMaxCount;
            _isFiring = false;
            _enemyProjectileSpawnTimerCounter = 0f;
        }
    }

    IEnumerator Pattern2Timer()
    {
        if (_enemyProjectilesRotationListIndex > 0)
        {
            EnemyProjectileSpawnPattern2();
            _enemyProjectilesRotationListIndex--;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Pattern2Timer());
        }
        else
        {
            _enemyProjectilesRotationListIndex = _enemyProjectileDirectionQuaternionList.Count;
            _isFiring = false;
            _enemyProjectileSpawnTimerCounter = 0f;
        }
    }

    private void EnemyControllerInitialization()
    {
        _enemyProjectilesperAttackCount = 12;
        _enemyProjectileDirectionQuaternionList = new List<Quaternion>();

        for (int i = 0; i < _enemyProjectilesperAttackCount; i++)
        {
            _enemyProjectileDirectionQuaternionList.Add(Quaternion.Euler(0, 0, (360 / _enemyProjectilesperAttackCount) * i));
        }

        _audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        _enemyProjectilesStack = GameObject.Find("EnemiesProjectiles").GetComponent<EnemiesProjectilesStack>();
        _enemyStacks = GameObject.Find("Enemies").GetComponent<EnemiesStacks>();
        _enemyProjectileToLaunch = null;
        _isFiring = false;
        _pattern1WaveMaxCount = 3;
        _pattern1WaveCount = _pattern1WaveMaxCount;
        _enemyProjectilesRotationListIndex = _enemyProjectileDirectionQuaternionList.Count;
        _enemyProjectileSpawnTimer = 1.5f;
        _enemyProjectileSpawnTimerCounter = 0f;
    }
}
