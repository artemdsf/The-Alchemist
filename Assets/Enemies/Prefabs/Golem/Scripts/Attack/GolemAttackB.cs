using System.Collections;
using UnityEngine;

public class GolemAttackB : GolemAttackState
{
	[Header("Attack 1")]
	[SerializeField] private Vector3 _attack1Pos;
	[SerializeField] private uint _damage = 5;
	[SerializeField] private uint _attackProjectilesCount = 8;
	[SerializeField] private string _attackName = "Attack";
	[SerializeField] private float _offset = 5;
	[SerializeField] private float _delayBetweenEpls = 0.2f;
	[Header("Run")]
	[SerializeField] private float _runSpeed;
	[SerializeField] private float _runTime;
	[SerializeField] private string _runName = "Run";
	[Header("Ability")]
	[SerializeField] private string _abilityName = "Ability";
	private GolemHealth _health;
	private bool _isAlreadyAttack = false;

	private const GolemState GOLEM_STATE = GolemState.B;

	public override void Init()
	{
		base.Init();
		ResetAttack();
		currentAttackDelay = 0;
	}

	protected override void Awake()
	{
		base.Awake();
		_health = GetComponent<GolemHealth>();
	}

	private void ResetAttack()
	{
		_isAlreadyAttack = false;
		StopRunning();
	}

	private void Attack1B()
	{
		StartCoroutine(AttackCoroutine());
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused && controller.CurentState == GOLEM_STATE && !_isAlreadyAttack)
		{
			currentAttackDelay += Time.deltaTime;
			if (_health.IsAbleToRebirth)
			{
				Rebirth();
			}
			else if (currentAttackDelay > attackDelay)
			{
				StartCoroutine(Attack());
				currentAttackDelay = 0;
			}
		}
	}

	private void Rebirth()
	{
		animator.SetTrigger(_abilityName);
		_health.Rebirth();
	}

	private void StartRunning()
	{
		if (controller.CurentState == GOLEM_STATE)
		{
			_isAlreadyAttack = true;
			controller.SetSpeed(_runSpeed);
			animator.SetBool(_runName, true);
			_health.DisactiveHitAnim();
		}
	}

	private void StopRunning()
	{
		controller.ResetSpeed();

		if (controller.CurentState == GOLEM_STATE)
			animator.SetBool(_runName, false);

		_health.ActiveHitAnim();
	}

	private void InstExplosion(Vector3 pos, Quaternion quaternion, uint damage)
	{
		GameObject gameObject = InstAttackObject(pos, quaternion);
		gameObject.TryGetComponent(out GolemExplosion explosion);
		explosion?.Init(damage);
	}

	private IEnumerator Attack()
	{
		StartRunning();

		yield return new WaitForSeconds(_runTime);

		StopRunning();

		if (controller.CurentState == GOLEM_STATE)
			animator.SetTrigger(_attackName);
	}

	private IEnumerator AttackCoroutine()
	{
		Vector3 curentPos = transform.position;
		Vector3 dir = (player.transform.position - curentPos).normalized;
		for (int i = 0; i < _attackProjectilesCount; i++)
		{
			InstExplosion(curentPos + _offset * (i + 1) * dir, Quaternion.identity, _damage);
			yield return new WaitForSeconds(_delayBetweenEpls);
		}
	}
}