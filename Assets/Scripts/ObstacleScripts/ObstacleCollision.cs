using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private float _obstacleStrength;
    // Start is called before the first frame update
    void Awake()
    {
        _obstacleStrength = 50.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLife>().TakeDamage(_obstacleStrength);
            Debug.Log("EMOTIONAL DAMAGE");
        }
    }
}
