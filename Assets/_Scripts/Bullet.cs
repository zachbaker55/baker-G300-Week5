using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float _despawnTimer;
    private float _currentTime;

    private float _shootSpeed;
    private int _damage;
    private Faction _faction;

    private Rigidbody2D _rigidBody;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        UpdateTimer();
    }

    public void Init(float shootSpeed, int damage, Faction faction) {
        _currentTime = 0;
        _faction = faction;
        _damage = damage;
        _shootSpeed = shootSpeed;
        if (faction == Faction.Player) {
            _rigidBody.velocity = _shootSpeed * new Vector2(0, 1).normalized;
        } else {
            _rigidBody.velocity = _shootSpeed * new Vector2(0, -1).normalized;
        }
    }

    private void UpdateTimer() {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _despawnTimer) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();
        if (attackable != null) {
            if (attackable.GetFaction() != _faction) {
                attackable.DoDamage(_damage);
            }
            Destroy(gameObject);
        }
    }

}
