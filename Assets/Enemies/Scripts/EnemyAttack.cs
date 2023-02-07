using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[Header("Attack properties")]
	[SerializeField] protected int damage = 1;

	public GameObject Player { get; private set; }

	protected Animator animator;

	private PlayerHealth _playerHealth;

	protected virtual void Awake()
	{
		Player = GameObject.FindGameObjectWithTag(Const.PlayerName);
		TryGetComponent(out animator);
		_playerHealth = Player.GetComponent<PlayerHealth>();
	}

	protected void HitPlayer()
	{
		_playerHealth.Damage(damage);
	}

	protected void HitPlayer(int damage)
	{
		_playerHealth.Damage(damage);
	}
}