using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerHealth), typeof(PlayerWeapon))]
public class PlayerController : MonoBehaviour
{
	private Transform _player;
	private Rigidbody2D _rb;
	private Vector3 _direction;
	private Vector3 _speed;

	[SerializeField] [Min(0)] private float _maxSpeed = 10;
	[SerializeField] [Range(0, 0.1f)] private float _lerpSpeed = 0.01f;

	[Header("Gizmos")]
	[SerializeField] private Color _barrierGizmosColor;
	private Vector2 _barrierSize;

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

		_speed = Vector2.Lerp(_speed, _direction.normalized * _maxSpeed, _lerpSpeed);

		_player.position += _speed * Time.deltaTime;
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
}
