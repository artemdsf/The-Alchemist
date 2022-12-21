using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	private GameObject _player;
	private PlayerHealth _playerHealth;

	[SerializeField] [Min(0)] private int _damage = 1;
	[SerializeField] [Min(0)] private float _attackDelay = 0.5f;
	private float _delay = 0;

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player");

		if (!_player.TryGetComponent<PlayerHealth>(out _playerHealth))
		{
			Debug.LogError("PlayerHealth not Found");
		}
	}

	private void Update()
	{
		_delay += Time.deltaTime;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			TryToHit();
		}

	}

	private void TryToHit()
	{
		if (_delay > _attackDelay)
		{
			_delay = 0;
			_playerHealth?.Damage(_damage);
		}
	}
}
