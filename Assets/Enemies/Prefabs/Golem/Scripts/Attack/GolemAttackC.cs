using System.Collections;
using UnityEngine;

public class GolemAttackC : GolemAttackState
{
	[Header("Ability")]
	[SerializeField] private string _abilityName = "Ability";
	[SerializeField] private int _damage = 5;
	[SerializeField] private int _abilityThornsCount = 4;
	[SerializeField] private float _directionsCount = 8;
	[SerializeField] private float _offset = 5;
	[SerializeField] private float _time = 2;
	[Header("Attack1")]
	[SerializeField] private string _attack1Name = "Attack";
	[SerializeField] private int _damage1 = 5;
	[SerializeField] private int _attackThornsCount = 8;
	[SerializeField] private float _attackOffset = 5;
	[SerializeField] private string _attack1PoolName;
	[Header("Attack2")]
	[SerializeField] private string _attack2Name = "Attack2";
	[SerializeField] private int _damage2 = 5;
	[SerializeField] private int _attackProjsCount = 4;
	[SerializeField] private float _startAngle = 45;
	[SerializeField] private Vector3 _firstPos;
	[SerializeField] private Vector3 _secondPos;
	[Header("Attack3")]
	[SerializeField] private float _slideSpeed = 5;
	[SerializeField] private float _slideLerpRatio = 1;
	[SerializeField] private string _attack3Name = "Attack3";
	[Header("Rebirth")]
	[SerializeField] private float _armor = 20;

	private ObjectPool _attack1Pool;

	private const GolemState GOLEM_STATE = GolemState.C;
	private GolemAttacksEnum _lastAtack = GolemAttacksEnum.Ability;

	private const int CIRCLE_DEGREES = 360;
	private const float CIRCLE_RADIANS = 2 * Mathf.PI;

	private bool _isAlreadyAttack;

	public override void Init()
	{
		base.Init();
		health.AddArmor(_armor);
		_isAlreadyAttack = false;
	}

	protected override void Awake()
	{
		base.Awake();

		_attack1Pool = GameObject.Find(_attack1PoolName)?.GetComponent<ObjectPool>();
	}

	private void Update()
	{
		if (controller.CurentState == GOLEM_STATE && !_isAlreadyAttack && !health.IsDead)
		{
			currentAttackDelay += Time.deltaTime;
			if (currentAttackDelay > attackDelay)
			{
				int attack = Random.Range(0, System.Enum.GetNames(typeof(GolemAttacksEnum)).Length);

				if (_lastAtack == (GolemAttacksEnum)attack)
					_lastAtack = (GolemAttacksEnum)((attack + 1) % System.Enum.GetNames(typeof(GolemAttacksEnum)).Length);
				else
					_lastAtack = (GolemAttacksEnum)attack;

				switch (_lastAtack)
				{
					case GolemAttacksEnum.Ability:
						Ability();
						break;
					case GolemAttacksEnum.Attack1:
						Attack1();
						break;
					case GolemAttacksEnum.Attack2:
						Attack2();
						break;
					case GolemAttacksEnum.Attack3:
						Attack3();
						break;
				}
				currentAttackDelay = 0;
			}
		}
	}
	
	private IEnumerator PauseAnim()
	{
		float tmp = animator.speed;

		animator.speed = 0;
		health.ActiveImmuneToDamage();
		_isAlreadyAttack = true;

		yield return new WaitForSeconds(_time);

		animator.speed = tmp;
		health.DisactiveImmuneToDamage();
		_isAlreadyAttack = false;
	}

	private IEnumerator StartAbilityAttack()
	{
		Vector3 curentPos = transform.position;
		float angleOffset = Random.Range(0, CIRCLE_RADIANS);

		for (int i = 0; i < _abilityThornsCount; i++)
		{
			for (int j = 0; j < _directionsCount; j++)
			{
				float angle = CIRCLE_RADIANS * j / _directionsCount + angleOffset;
				Vector3 dir = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

				InstThorns(curentPos + _offset * (i + 1) * dir, Quaternion.identity, _damage);
			}

			yield return new WaitForSeconds(_time / _abilityThornsCount);
		}
	}

	private void StartAttack1C()
	{
		Vector3 curentPos = transform.position;
		Vector3[] dirs = { Vector3.right, Vector3.left, Vector3.up, Vector3.down };

		foreach (var dir in dirs)
		{
			for (int i = 0; i < _attackThornsCount; i++)
			{
				InstThorns(curentPos + _attackOffset * (i + 1) * dir, Quaternion.identity, _damage1);
			}
		}
	}

	private void StartAttack2C1()
	{
		Vector3 curentPos;
		if (controller.IsOrientRight)
			curentPos = _firstPos + transform.position;
		else
			curentPos = InvertVectorByX(_firstPos) + transform.position;

		for (int i = 0; i < _attackProjsCount; i++)
		{
			Quaternion quaternion = Quaternion.Euler(0, 0, (CIRCLE_DEGREES * i / _attackProjsCount));
			InstProjectie(curentPos, quaternion, _damage2);
		}
	}

	private void StartAttack2C2()
	{
		Vector3 curentPos;
		if (controller.IsOrientRight)
			curentPos = _secondPos + transform.position;
		else
			curentPos = InvertVectorByX(_secondPos) + transform.position;

		for (int i = 0; i < _attackProjsCount; i++)
		{
			Quaternion quaternion = Quaternion.Euler(0, 0, (CIRCLE_DEGREES * i / _attackProjsCount + _startAngle));
			InstProjectie(curentPos, quaternion, _damage2);
		}
	}

	private void StartAttack3C()
	{
		StartCoroutine(controller.SetSpeed(_slideSpeed, _slideLerpRatio));
	}

	private void EndAttack3C()
	{
		StartCoroutine(controller.ResetSpeed(_slideLerpRatio));
	}

	private void StartBreak()
	{
		_isAlreadyAttack = true;
	}

	private void InstThorns(Vector3 pos, Quaternion quaternion, int damage)
	{
		GameObject gameObject = InstAttackObject(pos, quaternion, _attack1Pool);
		gameObject.TryGetComponent(out GolemExplosion explosion);
		explosion?.Init(damage);
	}

	private void InstProjectie(Vector3 pos, Quaternion quaternion, int damage)
	{
		GameObject gameObject = InstAttackObject(pos, quaternion);
		gameObject.TryGetComponent(out GolemProjectile projectile);
		projectile?.Init(damage);
	}

	private void Ability()
	{
		if (controller.CurentState == GOLEM_STATE)
			animator.SetTrigger(_abilityName);
	}

	private void Attack1()
	{
		if (controller.CurentState == GOLEM_STATE)
			animator.SetTrigger(_attack1Name);
	}

	private void Attack2()
	{
		if (controller.CurentState == GOLEM_STATE)
			animator.SetTrigger(_attack2Name);
	}

	private void Attack3()
	{
		if (controller.CurentState == GOLEM_STATE)
			animator.SetTrigger(_attack3Name);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position + _firstPos, "Center", false, Color.red);
		Gizmos.DrawIcon(transform.position + _secondPos, "Center", false, Color.green);
	}
}