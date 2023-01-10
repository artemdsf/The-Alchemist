using UnityEngine;

public class GolemAttackA : MonoBehaviour
{
	[SerializeField] private GameObject _projectile;
	[Header("Attack 2")]
	[SerializeField] private Vector3 _attack2Pos;
	[SerializeField] private uint _damage2 = 5;
	[SerializeField] private uint _attack2ProjectilesCount = 8;
	[Header("Attack 3")]
	[SerializeField] private Vector3 _attack3Pos;
	[SerializeField] private uint _damage3 = 5;
	[SerializeField] private uint _attack3ProjectilesCount = 4;
	[SerializeField] [Min(0)] private float _attack3MaxAngle = 30f;
	[Header("Attack delay")]
	[SerializeField] private float _maxAttackDelay = 1;
	private float _attackDelay;

	private GolemController _controller;
	private GameObject _player;

	private const int CIRCLE_DEGREES = 360;

	//Circular attack
	protected void Attack2A()
	{
		float startAngle = Random.Range(0, CIRCLE_DEGREES);
		for (int i = 0; i < _attack2ProjectilesCount; i++)
		{
			Quaternion quaternion = Quaternion.Euler(Vector3.forward * (CIRCLE_DEGREES * i / _attack2ProjectilesCount + startAngle));

			if (_controller.IsOrientRight)
				InstProjectile(transform.position + _attack2Pos, quaternion, _damage2);
			else
				InstProjectile(transform.position + InvertVectorByX(_attack2Pos), quaternion, _damage2);
		}
	}

	//Directed attack
	protected void Attack3A()
	{
		for (int i = 0; i < _attack3ProjectilesCount; i++)
		{
			float angleDelta;
			Quaternion quaternion = Quaternion.identity;

			angleDelta = _attack3MaxAngle * 2 / (_attack3ProjectilesCount - 1);

			if (_controller.IsOrientRight)
			{
				if (_attack3ProjectilesCount > 1)
					quaternion = Quaternion.Euler(Vector3.forward * (angleDelta * i - _attack3MaxAngle));
				InstProjectile(transform.position + _attack3Pos, quaternion, _damage3);
			}
			else
			{
				if (_attack3ProjectilesCount > 1)
					quaternion = Quaternion.Euler(Vector3.forward * (angleDelta * i - _attack3MaxAngle - CIRCLE_DEGREES / 2));
				InstProjectile(transform.position + InvertVectorByX(_attack3Pos), quaternion, _damage3);
			}
		}
	}

	private void Awake()
	{
		_controller = GetComponent<GolemController>();
	}

	private void Start()
	{
		_player = GetComponent<GolemAttack>().Player;
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused && _controller.CurentState == GolemState.A)
		{
			_attackDelay += Time.deltaTime;

			if (_attackDelay > _maxAttackDelay)
			{
				Attack();
			}
		}
	}

	public void Attack()
	{
		Vector3 attackDir = _player.transform.position - transform.position;
		float angle;
		if (_controller.IsOrientRight)
			angle = Vector3.Angle(Vector3.right, attackDir);
		else
			angle = Vector3.Angle(Vector3.left, attackDir);

		if (angle > _attack3MaxAngle)
			_controller.Animator.SetTrigger("Attack 2");
		else
			_controller.Animator.SetTrigger("Attack 3");

		_attackDelay = 0;
	}

	private void InstProjectile(Vector3 pos, Quaternion quaternion, uint damage)
	{
		GameObject gameObject = Instantiate(_projectile, pos, quaternion);
		gameObject.TryGetComponent(out ProjectileController projectile);
		projectile?.Init(damage);
	}

	private Vector3 InvertVectorByX(Vector3 vector)
	{
		return new Vector3(-vector.x, vector.y, vector.z);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position + _attack2Pos, "Center", false, Color.red);
		Gizmos.DrawIcon(transform.position + _attack3Pos, "Center", false, Color.green);
	}
}