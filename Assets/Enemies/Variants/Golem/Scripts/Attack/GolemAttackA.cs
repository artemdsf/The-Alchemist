using UnityEngine;

public class GolemAttackA : GolemAttackState
{
	[Header("Attack 2")]
	[SerializeField] private Vector3 _attack2Pos;
	[SerializeField] private int _damage2 = 5;
	[SerializeField] private int _attack2ProjectilesCount = 8;
	[Header("Attack 3")]
	[SerializeField] private Vector3 _attack3Pos;
	[SerializeField] private int _damage3 = 5;
	[SerializeField] private int _attack3ProjectilesCount = 4;
	[SerializeField] [Min(0)] private float _attack3MaxAngle = 30;
	[Header("Ability")]
	[SerializeField] private float _abilityDelay = 10;
	private float _currentAbilityDelay = 0;

	private const GolemState GOLEM_STATE = GolemState.A;

	private const int CIRCLE_DEGREES = 360;

	public override void Init()
	{
		base.Init();
		_currentAbilityDelay = 0;
	}

	//Circular attack
	private void Attack2A()
	{
		float startAngle = Random.Range(0, CIRCLE_DEGREES);
		for (int i = 0; i < _attack2ProjectilesCount; i++)
		{
			Quaternion quaternion = Quaternion.Euler(Vector3.forward * (CIRCLE_DEGREES * i / _attack2ProjectilesCount + startAngle));

			if (controller.IsOrientRight)
				InstProjectile(transform.position + _attack2Pos, quaternion, _damage2);
			else
				InstProjectile(transform.position + InvertVectorByX(_attack2Pos), quaternion, _damage2);
		}
	}

	//Directed attack
	private void Attack3A()
	{
		for (int i = 0; i < _attack3ProjectilesCount; i++)
		{
			float angleDelta;
			Quaternion quaternion = Quaternion.identity;

			angleDelta = _attack3MaxAngle * 2 / (_attack3ProjectilesCount - 1);

			if (controller.IsOrientRight)
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

	private void AbilityA()
	{
		animator.SetTrigger(Const.AbilityName);
	}

	private void Update()
	{
		if (controller.CurentState == GOLEM_STATE && !health.IsDead)
		{
			currentAttackDelay += Time.deltaTime;
			_currentAbilityDelay += Time.deltaTime;

			if (currentAttackDelay > attackDelay)
			{
				Attack();
				currentAttackDelay = 0;
			}
			if (_currentAbilityDelay > _abilityDelay)
			{
				AbilityA();
				_currentAbilityDelay = 0;
			}
		}
	}

	public void Attack()
	{
		Vector3 attackDir = player.transform.position - transform.position;
		float angle;
		if (controller.IsOrientRight)
			angle = Vector3.Angle(Vector3.right, attackDir);
		else
			angle = Vector3.Angle(Vector3.left, attackDir);

		if (angle > _attack3MaxAngle)
		{
			animator.SetTrigger(Const.Attack2Name);
			animator.ResetTrigger(Const.Attack3Name);
		}
		else
		{
			animator.SetTrigger(Const.Attack3Name);
			animator.ResetTrigger(Const.Attack2Name);
		}
	}

	private void InstProjectile(Vector3 pos, Quaternion quaternion, int damage)
	{
		GameObject gameObject = InstAttackObject(pos, quaternion);
		gameObject.TryGetComponent(out GolemProjectile projectile);
		projectile?.Init(damage);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position + _attack2Pos, "Center", false, Color.red);
		Gizmos.DrawIcon(transform.position + _attack3Pos, "Center", false, Color.green);
	}
}