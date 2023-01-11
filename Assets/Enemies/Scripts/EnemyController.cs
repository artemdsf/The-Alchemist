using UnityEngine;

[RequireComponent(typeof(EnemyColor), typeof(EnemyHealth), typeof(EnemyAttack))]
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
	[SerializeField] [Min(0)] private float _speed = 1f;

	public Animator _animator { get; private set; }

	public ElementEnum Element { get; private set; }

	public bool IsOrientRight { get; private set; }

	protected bool isAlive => !_health.IsDead;

	protected GameObject player;

	private Rigidbody2D _rb;
	private EnemyHealth _health;
	private EnemyColor _enemyColor;

	private Color _color = Color.white;

	private Vector3 _scale = Vector3.one;


	public void Init(ElementEnum element, Color color)
	{
		Element = element;
		_color = color;
		_health.Init(Element);
	}

	protected virtual void Awake()
	{
		_health = GetComponent<EnemyHealth>();
		_rb = GetComponent<Rigidbody2D>();
		_scale = transform.localScale;
	}

	protected virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		TryGetComponent(out _enemyColor);
		_enemyColor?.ChangeColor(_color);
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
			Move(player.transform.position - transform.position);
		}
		else
		{
			Move(player.transform.position - transform.position, 0);
		}
	}

	protected void Move(Vector3 dir)
	{
		dir = dir.normalized;
		_rb.velocity = dir * _speed;
	}

	protected void Move(Vector3 pos, float speed)
	{
		Vector2 dir = (pos - transform.position).normalized;
		_rb.velocity = dir * speed;
	}

	protected void SetOrientation()
	{
		if (player.transform.position.x - transform.position.x > 0 && !IsOrientRight)
		{
			transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
			IsOrientRight = true;
		}
		else if (player.transform.position.x - transform.position.x < 0 && IsOrientRight)
		{
			transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
			IsOrientRight = false;
		}
	}
}