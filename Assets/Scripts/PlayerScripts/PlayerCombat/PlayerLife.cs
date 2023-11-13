using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLife : Stats
{
    [SerializeField] private float _treshhold;

    private Transform _player;

    private Vector3 _spawnPoint;

    public delegate void PlayerIsDead();
    public event PlayerIsDead playerDead;
    
    private void Awake()
    {
        _player = GetComponent<Transform>();
        _treshhold = -5f;
        _spawnPoint = _player.position;
        //Default stats
        _health = 100.0f;
        _armor = 1.0f;
        _attackSpeed = 2.0f;
        _strength = 10.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PrevLevel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //_player.transform.position = _spawnPoint;
        _health = 100.0f;
        playerDead?.Invoke();
    }

    void Update()
    {
       // Debug.Log("Player health: " + _health);
        if (_player.position.y < _treshhold)
        {
            Die();
        }
    }
}
