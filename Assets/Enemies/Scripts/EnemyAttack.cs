using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[Header("Attack properties")]
	[SerializeField] protected int damage = 1;

	public GameObject Player { get; private set; }

	private PlayerHealth _playerHealth;

	protected void HitPlayer()
	{
		_playerHealth?.Damage(damage);
	}

	private void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		_playerHealth = Player.GetComponent<PlayerHealth>();
	}
}