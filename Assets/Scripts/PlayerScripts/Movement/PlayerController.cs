using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    #region Classes
    private TouchControlls _playerInputController;
    private CharacterController _controller;
    #endregion


    #region Variables
    private Vector2 _startedPosition = Vector2.zero;
    private Vector2 _currentPosition = Vector2.zero;
    private Vector3 _moveDirection;

    private bool _isInCombat;
    #endregion

    #region Properties
    [Header("Swipe Detection Properties")]
    [Space]
    [SerializeField] private byte _swipeThreshold;

    [Header("Player Movement Properties")]
    [Space]
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _runningSpeed;
    #endregion

    private void Start() 
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        }
        else{
            Debug.Log("GO FUCK URSELF");
        }
        _isInCombat = false;
        // /DontDestroyOnLoad(this);
        _jumpVelocity = 8f;
        _gravityScale = 2f;
        // Instantiating input system object
        _playerInputController = new TouchControlls();

        //Instantiating player controll object
        //_controller = GetComponent<CharacterController>();
        // Better enabling input actions in action map
        var touchInteraction = _playerInputController.TouchInteraction;
        touchInteraction.Enable();    

        touchInteraction.TouchPosition.performed += ctx => { _currentPosition = touchInteraction.TouchPosition.ReadValue<Vector2>(); };
        touchInteraction.TouchPress.started += ctx => {_startedPosition = _currentPosition; };
        touchInteraction.TouchPress.canceled += SwipeDetection;
    }

    public bool CombatStatus
    {
        get{return _isInCombat;}
        set
        {
            _isInCombat = value;
        }
    }
    private void SwipeDetection(InputAction.CallbackContext context)
    {
        Vector2 delta = _currentPosition - _startedPosition;
        
        if(delta.magnitude < _swipeThreshold)
        {
            ProduceJump();
            return;
        }

        if((Mathf.Abs(delta.x) < Mathf.Abs(delta.y)) && (_controller.isGrounded) && (!_isInCombat))
        {
            Debug.Log("Vertical Swipe");
            if (delta.y > 0)
            {
                Debug.Log("Swipe Up" + delta.y + " " + _startedPosition.y);
                //if (SwipeUpDetected != null) SwipeUpDetected(context);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector2.up, out hit, 5))
                {
                    Teleport(new Vector3(transform.position.x, hit.point.y + 1f, transform.position.z));
                }
            }
            else
            {
                Debug.Log("Swipe Down" + delta.y + " " + _startedPosition.y);
                //if (SwipeDownDetected != null) SwipeDownDetected(context);
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector2.down, out hit, 7))
                {
                    Teleport(new Vector3(transform.position.x, hit.point.y + 1f, transform.position.z));
                }
            }
        }

    }

    private void Teleport(Vector3 _position)
    {
        _controller.transform.position = _position;
    }

    private void ProduceJump(/*InputAction.CallbackContext context*/)
    {
        //if (JumpInitiated != null) JumpInitiated(context);
        if (_controller.isGrounded && !_isInCombat)
        {
            _moveDirection.y = _jumpVelocity;
        }
    }

    private void Update()
    {
        if(!_isInCombat)
        {
            _moveDirection = new Vector3(_runningSpeed, _moveDirection.y, 0f);
            _moveDirection.y += Physics.gravity.y * _gravityScale * Time.deltaTime;
            Physics.SyncTransforms();
            _controller.Move(_moveDirection * Time.deltaTime);
        }
        else
        {
            _moveDirection.y = 0.0f;
        }
    }
}


