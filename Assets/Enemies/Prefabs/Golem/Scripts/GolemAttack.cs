using UnityEngine;

public class GolemAttack : EnemyAttack
{
	[SerializeField] private GameObject _projectile;
	[Header("Attack 2")]
	[SerializeField] private Vector3 _attack2Pos;
	[SerializeField] private uint _attack2ProjectilesCount = 8;
	[Header("Attack 3")]
	[SerializeField] private Vector3 _attack3Pos;
	[SerializeField] private uint _attack3ProjectilesCount = 4;
	[SerializeField] [Min(0)] private float _attack3MaxAngle = 30f;

	private EnemyController _controller;
	private Animator _animator;

	private const int CIRCLE_DEGREES = 360;

	private void Awake()
	{
		_controller = GetComponent<EnemyController>();
	}

	protected void Attack2()
	{
		for (int i = 0; i < _attack2ProjectilesCount; i++)
		{
			Quaternion quaternion = Quaternion.Euler(Vector3.forward * CIRCLE_DEGREES * i / _attack2ProjectilesCount);

			if (_controller.IsOrientRight)
				InstProjectile(transform.position + _attack2Pos, quaternion);
			else
				InstProjectile(transform.position + InvertVectorByX(_attack2Pos), quaternion);
		}
	}

	protected void Attack3()
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
				InstProjectile(transform.position + _attack3Pos, quaternion);
			}
			else
			{
				if (_attack3ProjectilesCount > 1)
					quaternion = Quaternion.Euler(Vector3.forward * (angleDelta * i - _attack3MaxAngle - CIRCLE_DEGREES / 2));
				InstProjectile(transform.position + InvertVectorByX(_attack3Pos), quaternion);
			}
		}
	}

	private void InstProjectile(Vector3 pos, Quaternion quaternion)
	{
		GameObject gameObject = Instantiate(_projectile, pos, quaternion);
		gameObject.TryGetComponent(out ProjectileController projectile);
		projectile?.Init(damage);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position + _attack2Pos, "Center", false, Color.red);
		Gizmos.DrawIcon(transform.position + _attack3Pos, "Center", false, Color.green);
	}

	private Vector3 InvertVectorByX(Vector3 vector)
	{
		return new Vector3(-vector.x, vector.y, vector.z);
	}
}