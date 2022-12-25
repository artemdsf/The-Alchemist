using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyHealth), typeof(EnemyAttack))]
public class EnemyController : MonoBehaviour
{
	private GameObject _player;
	private Rigidbody2D _rb;
	private EnemyHealth _health;
	private Vector3 _scale;

	[SerializeField] [Min(0)] private float _speed = 1f;

	private void Awake()
	{
		_health = GetComponent<EnemyHealth>();
	}

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = 0;
		_scale = transform.localScale;
	}

	private void FixedUpdate()
	{
		if (!GameManager.IsGamePaused && !_health.IsDead)
		{
			Vector2 dir = (_player.transform.position - transform.position).normalized;
			_rb.velocity = dir * _speed;

			if (_rb.velocity.x < 0)
			{
				transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
			}
			else
			{
				transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
			}
		}
		else
		{
			_rb.velocity = Vector2.zero;
		}
	}
}