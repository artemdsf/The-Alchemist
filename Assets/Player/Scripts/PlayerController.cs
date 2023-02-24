using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerHealth), typeof(PlayerWeaponManager))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] [Min(0)] private float _maxSpeed = 10;
	[SerializeField] private float _lerpSpeed = 10f;

	[Header("Gizmos")]
	[SerializeField] private Color _barrierGizmosColor;
	private Vector2 _barrierSize;

	[Header("Animator")]
	[SerializeField] private Animator _animator;

	private Transform _player;
	private Rigidbody2D _rb;
	private Vector3 _direction;
	private Vector3 _speed;
	private Vector3 _scale = Vector3.one;
	private bool _isOrientRight = true;
	private bool _isRun = false;

	private void Awake()
	{
		_scale = transform.localScale;
	}

	private void Start()
	{
		_player = GetComponent<Transform>();
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = 0;

		float cameraSize = Camera.main.orthographicSize;
		_barrierSize = new Vector2(GameManager.FieldSize.x - cameraSize * 2, GameManager.FieldSize.y - cameraSize);
	}

	private void Update()
	{
		Move();
		CheckBarrier();
	}

	private void Move()
	{
		_direction.x = Input.GetAxisRaw("Horizontal");
		_direction.y = Input.GetAxisRaw("Vertical");

		_speed = Vector2.Lerp(_speed, _direction.normalized * _maxSpeed, _lerpSpeed * Time.deltaTime);

		_player.position += _speed * Time.deltaTime;

		SetOrientation(_direction.x);

		if (_direction == Vector3.zero && _isRun)
		{
			_animator.SetBool(Const.RunName, false);
			_isRun = false;
		}
		else if (_direction != Vector3.zero && !_isRun)
		{
			_animator.SetBool(Const.RunName, true);
			_isRun = true;
		}
	}

	private void CheckBarrier()
	{
		if (_player.position.x < -_barrierSize.x)
		{
			_player.position = new Vector3(-_barrierSize.x, _player.position.y, _player.position.z);
		}
		if (_player.position.x > _barrierSize.x)
		{
			_player.position = new Vector3(_barrierSize.x, _player.position.y, _player.position.z);
		}
		if (_player.position.y < -_barrierSize.y)
		{
			_player.position = new Vector3(_player.position.x, -_barrierSize.y, _player.position.z);
		}
		if (_player.position.y > _barrierSize.y)
		{
			_player.position = new Vector3(_player.position.x, _barrierSize.y, _player.position.z);
		}
	}

	private void SetOrientation(float x)
	{
		if (x > 0 && !_isOrientRight)
		{
			transform.localScale = _scale;
			_isOrientRight = true;
		}
		else if (x < 0 && _isOrientRight)
		{
			transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
			_isOrientRight = false;
		}
	}
}
