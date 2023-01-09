using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[Header("Attack properties")]
	[SerializeField] protected uint damage = 1;

	protected GameObject player;

	private PlayerHealth _playerHealth;

	protected void HitPlayer()
	{
		_playerHealth?.Damage(damage);
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		_playerHealth = player.GetComponent<PlayerHealth>();
	}
}