using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
	[Header("Attack properties")]
	[SerializeField] [Min(0)] private int _damage = 1;

	protected GameObject player;

	private PlayerHealth _playerHealth;

	public void Init(GameObject player)
	{
		this.player = player;
	}

	protected void HitPlayer()
	{
		_playerHealth.Damage(_damage);
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		_playerHealth = player.GetComponent<PlayerHealth>();
	}
}