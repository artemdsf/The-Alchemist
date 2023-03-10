using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyColor), typeof(EnemyHealth), typeof(EnemyAttack))]
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
	[SerializeField] private Vector3 _pivot = Vector3.zero;
	[SerializeField] [Min(0)] private float _speed = 1f;
	private float _currentSpeed = 1f;
	
	public Vector3 Pivot => _pivot;

	public ElementEnum Element { get; private set; }

	public bool IsOrientRight { get; private set; }

	protected bool isAlive => !_health.IsDead;

	protected GameObject player;

	private Rigidbody2D _rb;
	private EnemyHealth _health;
	private EnemyColor _enemyColor;

	private Vector3 _scale = Vector3.one;
	private Vector3 _pos = Vector3.one;

	private float _speedError = 0.01f;

	public void ResetSpeed()
	{
		_currentSpeed = _speed;
	}

	public IEnumerator ResetSpeed(float lerpRatio)
	{
		while (Mathf.Abs(1 - _speed / _currentSpeed) > _speedError)
		{
			_currentSpeed = Mathf.Lerp(_currentSpeed, _speed, lerpRatio * Time.deltaTime);
			yield return null;
		}
		_currentSpeed = _speed;
	}

	public void SetSpeed(float speed)
	{
		_currentSpeed = speed;
	}

	public IEnumerator SetSpeed(float speed, float lerpRatio)
	{
		while (Mathf.Abs(1 - speed / _currentSpeed) > _speedError)
		{
			_currentSpeed = Mathf.Lerp(_currentSpeed, speed, lerpRatio * Time.deltaTime);
			yield return null;
		}
		_currentSpeed = speed;
	}

	public virtual void SetElement(ElementEnum element, Color color)
	{
		Element = element;
		_enemyColor.ChangeColor(color);
		_health.SetElement(Element);
	}

	protected virtual void Awake()
	{
		_health = GetComponent<EnemyHealth>();
		_enemyColor = GetComponent<EnemyColor>();
		_rb = GetComponent<Rigidbody2D>();
		_scale = transform.localScale;
		IsOrientRight = true;
	}

	protected virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag(Const.PlayerName);

		SetOrientation();
	}

	protected virtual void Update()
	{
		if (_speed != 0)
		{
			SetOrientation();
		}
		_pos = transform.position + _pivot;
	}

	protected virtual void FixedUpdate()
	{
		if (isAlive)
		{
			Move(player.transform.position - _pos);
		}
		else
		{
			Move(player.transform.position - _pos, 0);
		}
	}

	protected void Move(Vector3 dir)
	{
		dir = dir.normalized;
		_rb.velocity = dir * _currentSpeed;
	}

	protected void Move(Vector3 pos, float speed)
	{
		Vector2 dir = (pos - _pos).normalized;
		_rb.velocity = dir * speed;
	}

	private void OnEnable()
	{
		ResetSpeed();
		_pos = transform.position + _pivot;
	}

	private void SetOrientation()
	{
		if (player.transform.position.x - _pos.x > 0 && !IsOrientRight)
		{
			transform.localScale = _scale;
			IsOrientRight = true;
		}
		else if (player.transform.position.x - _pos.x < 0 && IsOrientRight)
		{
			transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
			IsOrientRight = false;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(_pivot + transform.position, "Pivot", false);
	}
}