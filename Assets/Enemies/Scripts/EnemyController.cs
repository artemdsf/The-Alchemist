using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyHealth), typeof(EnemyAttack))]
public class EnemyController : MonoBehaviour
{
	private GameObject _player;
	private Rigidbody2D _rb;

	[SerializeField] [Min(0)] private float _speed = 1f;

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = 0;

	}

	private void FixedUpdate()
	{
		Vector2 dir = (_player.transform.position - transform.position).normalized;
		_rb.velocity = dir * _speed;
	}
}
