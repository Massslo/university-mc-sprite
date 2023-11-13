using Unity.VisualScripting;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    
    //private CharacterController _playerController;
    private PlayerController _playerController;
    private EnemyLife _enemy;
    private PlayerLife _player;

    private float _nextPlayerDamageEvent;
    private float _nextEnemyDamageEvent;
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _player = GetComponent<PlayerLife>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _playerController.CombatStatus = true;
            //_enemy = other.gameObject.GetComponent<EnemyLife>();
            if(other.gameObject.TryGetComponent<EnemyLife>(out _enemy))
            {
                _playerController.transform.position = new Vector3(_enemy.transform.position.x - 1.0f, _player.transform.position.y, _player.transform.position.z);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_playerController.CombatStatus)
        {
            if(_enemy != null)
            {
                if(Time.time >= _nextPlayerDamageEvent)
                {
                    _nextPlayerDamageEvent = Time.time + _player.AttackSpeed;
                    _enemy.TakeDamage(_player.DoDamage());
                }
                if(Time.time >= _nextEnemyDamageEvent)
                {
                    _nextEnemyDamageEvent = Time.time + _enemy.AttackSpeed;
                    _player.TakeDamage(_enemy.DoDamage());
                }
            }
            else
            {
                _playerController.CombatStatus = false;
            }
        }
        //if(_playerCollider.SendMessage);
    }
}
