using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IAttackable {

    [SerializeField] private int _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _shotSpeed;
    [SerializeField] private int _shotDamage;
    [SerializeField] private Faction _faction;
    
    [SerializeField] private GameObject _shotPrefab;


    private float _movement = 0;

    private Rigidbody2D _rigidBody;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _movement = Input.GetAxisRaw("Horizontal");
        AttemptShot();
    }

    private void FixedUpdate() {
        DoMovement();
    }

    private void DoMovement() {
        if (_movement != 0) {
            _rigidBody.velocity = _moveSpeed * new Vector2(_movement, 0).normalized;
        } else {
            _rigidBody.velocity = Vector2.zero;
        }
    }

    private void AttemptShot() {
        if (Input.GetButtonDown("Shoot")) {
            GameObject shot = Instantiate(_shotPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
            Bullet bullet = shot.GetComponent<Bullet>();
            bullet.Init(_shotSpeed, _shotDamage, _faction);
        }
    }

    public void DoDamage(int damage) {
        _health -= damage;
        if (_health <= 0) {
            Debug.Log("Player has been killed.");
            Destroy(gameObject);
        }
    }

    public Faction GetFaction() {
        return _faction;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            Destroy(gameObject);
        }
    }

}
