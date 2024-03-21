using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TDController : MonoBehaviour
{
    private Animator _animator;
    private Vector2 _playerInputVec;
    private Vector3 _playerMovement;
    private readonly float _rotateSpeed = 1000f;
    private Transform _playerTrans;
    private float _currentSpeed;
    private float _targertSpeed;
    private float _walkSpeed = 1.5f;
    private float _runSpeed = 5.5f;
    

    private bool _bRunning;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MovePlayer();
    }

    public void GetPlayerMoveInput(InputAction.CallbackContext ctx)
    {
        _playerInputVec = ctx.ReadValue<Vector2>();
    }
    
    public void GetPlayerRunInput(InputAction.CallbackContext ctx)
    {
        _bRunning = ctx.ReadValue<float>() > 0;
    }

    void RotatePlayer()
    {
        if (_playerInputVec.Equals(Vector2.zero))
        {
            return;
        }
        _playerMovement.x = _playerInputVec.x;
        _playerMovement.z = _playerInputVec.y;
        
        Quaternion targetRotation = Quaternion.LookRotation(_playerMovement, Vector3.up);
        _playerTrans.rotation = Quaternion.RotateTowards(_playerTrans.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        _targertSpeed = _bRunning ? _runSpeed : _walkSpeed;
        _targertSpeed *= _playerInputVec.magnitude;
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targertSpeed, 0.5f);
        _animator.SetFloat("Vertical Speed", _currentSpeed);
    }
}
