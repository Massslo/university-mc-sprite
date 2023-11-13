using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThePlayer : MonoBehaviour
{
    private GameObject _player;
    public GameObject _playerPrefab;
    public GameObject _cameraPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        _player = Instantiate(_playerPrefab);
        Instantiate(_cameraPrefab);
        _player.transform.position = transform.position;
    }
}
