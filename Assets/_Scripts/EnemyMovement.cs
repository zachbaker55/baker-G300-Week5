using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _horizontalTime;
    [SerializeField] private float _verticalTime;

    private float _currentTime;
    private int _currentPhase;
    private Rigidbody2D _rigidBody;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void Start() {
        _currentTime = _horizontalTime/2.0f;
        _currentPhase = 0;
        _rigidBody.velocity = _moveSpeed * new Vector2(-1, 0).normalized;
    }

    private void Update() {
        DoPhase();
    }

    private void DoPhase() {
        _currentTime += Time.deltaTime;
        if (_currentPhase == 0) { //Left phase moving to Down phase
            if (_currentTime >= _horizontalTime) {
                _currentTime = 0;
                _currentPhase = 1;
                _rigidBody.velocity = _moveSpeed * new Vector2(0, -1).normalized;
            }
        }
        if (_currentPhase == 1) { //Down phase moving to Right phase
            if (_currentTime >= _verticalTime) {
                _currentTime = 0;
                _currentPhase = 2;
                _rigidBody.velocity = _moveSpeed * new Vector2(1, 0).normalized;
            }
        }
        if (_currentPhase == 2) { //Right phase moving to Down2 phase
            if (_currentTime >= _horizontalTime) {
                _currentTime = 0;
                _currentPhase = 3;
                _rigidBody.velocity = _moveSpeed * new Vector2(0, -1).normalized;
            }
        }
        if (_currentPhase == 3) { //Down2 phase moving to Left phase
            if (_currentTime >= _verticalTime) {
                _currentTime = 0;
                _currentPhase = 0;
                _rigidBody.velocity = _moveSpeed * new Vector2(-1, 0).normalized;
            }
        }
    }
}
