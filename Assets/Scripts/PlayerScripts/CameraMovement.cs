using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Transform _target;

    [SerializeField] private Vector3 _offset; //4.5
    private float _lowLevelCamera;
    private float _highLevelCamera;
    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else{
            Debug.Log("AM DUBM");
        }
        transform.position = new Vector3(_target.transform.position.x - _offset.x, _target.transform.position.y - _offset.y, _target.transform.position.z - _offset.z);
        _lowLevelCamera = transform.position.y + 4.6f;
        _highLevelCamera = _lowLevelCamera + 5.5f/*3.92f*/;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = target.transform.position - offset;
        //transform.position = new Vector3(_target.transform.position.x - _offset.x, _controller.transform.position.y, _target.transform.position.z - _offset.z);//target.transform.position - offset;
        if(_target.transform.position.y > 7.8f)
        {
            transform.position = new Vector3(_target.transform.position.x - _offset.x, _highLevelCamera, _target.transform.position.z - _offset.z);
        }
        else
        {
            transform.position = new Vector3(_target.transform.position.x - _offset.x, _lowLevelCamera, _target.transform.position.z - _offset.z);
        }
    }
}
