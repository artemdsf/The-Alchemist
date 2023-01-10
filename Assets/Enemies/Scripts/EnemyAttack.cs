using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[Header("Attack properties")]
	[SerializeField] protected uint damage = 1;

	public GameObject Player { get; private set; }

	private PlayerHealth _playerHealth;

	protected void HitPlayer()
	{
		_playerHealth?.Damage(damage);
	}

	private void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		_playerHealth = Player.GetComponent<PlayerHealth>();
	}
}