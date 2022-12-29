using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[Header("Attack properties")]
	[SerializeField] [Min(0)] private int _damage = 1;

	protected PlayerHealth _playerHealth;
	protected GameObject _player;

	protected void Hit()
	{
		_playerHealth.Damage(_damage);
	}

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_playerHealth = _player.GetComponent<PlayerHealth>();
	}
}