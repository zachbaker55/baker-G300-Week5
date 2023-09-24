using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour, IAttackable {

    [SerializeField] private int _health;
    [SerializeField] private int _scoreDropped;
    [SerializeField] private float _shotTimerRange;
    [SerializeField] private float _shotSpeed;
    [SerializeField] private int _shotDamage;
    [SerializeField] private Faction _faction;
    
    [SerializeField] private GameObject _shotPrefab;

    private float _shotTimer;
    private float _currentTime;

    private Rigidbody2D _rigidBody;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        PrepareShot();
    }

    private void Update() {
        CheckShot();
    }

    private void PrepareShot() {
        _currentTime = 0;
        _shotTimer = UnityEngine.Random.Range(0, _shotTimerRange);
    }

    private void CheckShot() {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _shotTimer) {
            GameObject shot = Instantiate(_shotPrefab, transform.position + new Vector3(0,-1,0), Quaternion.identity);
            Bullet bullet = shot.GetComponent<Bullet>();
            bullet.Init(_shotSpeed, _shotDamage, _faction);
            PrepareShot();
        }
    }

    public void DoDamage(int damage) {
        _health -= damage;
        if (_health <= 0) {
            GameManager.ChangeScore(_scoreDropped);
            Destroy(gameObject);
        }
    }

    public Faction GetFaction() {
        return _faction;
    }

}
