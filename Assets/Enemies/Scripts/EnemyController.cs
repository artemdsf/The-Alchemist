using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyHealth), typeof(EnemyAttack))]
[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
	[SerializeField] [Min(0)] protected float _speed = 1f;

	protected bool isAlive => !_health.IsDead;

	protected GameObject player;

	private Rigidbody2D _rb;
	private EnemyHealth _health;
	private Vector3 _scale;

	private bool _isOrientRight;

	protected virtual void Awake()
	{
		_health = GetComponent<EnemyHealth>();
		_rb = GetComponent<Rigidbody2D>();
		_scale = transform.localScale;
	}

	protected virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	protected virtual void Update()
	{
		if (!GameManager.IsGamePaused)
		{
			SetOrientation();
		}
	}

	protected virtual void FixedUpdate()
	{
		if (!GameManager.IsGamePaused && isAlive)
		{
			Run(player.transform.position - transform.position);
		}
		else
		{
			Run(player.transform.position - transform.position, 0);
		}
	}

	protected void Run(Vector3 dir)
	{
		dir = dir.normalized;
		_rb.velocity = dir * _speed;
	}

	protected void Run(Vector3 pos, float speed)
	{
		Vector2 dir = (pos - transform.position).normalized;
		_rb.velocity = dir * speed;
	}

	protected void SetOrientation()
	{
		if (_rb.velocity.x > 0 && _isOrientRight)
		{
			transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
			_isOrientRight = false;
		}
		else if (_rb.velocity.x < 0 && !_isOrientRight)
		{
			transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
			_isOrientRight = true;
		}
	}
}