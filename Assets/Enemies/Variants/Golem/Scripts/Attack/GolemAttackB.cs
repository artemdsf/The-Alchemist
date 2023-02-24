using System.Collections;
using UnityEngine;

public class GolemAttackB : GolemAttackState
{
	[Header("Attack 1")]
	[SerializeField] private Vector3 _attack1Pos;
	[SerializeField] private int _damage = 5;
	[SerializeField] private int _attackProjectilesCount = 8;
	[SerializeField] private float _offset = 5;
	[SerializeField] private Vector3 _startOffset = Vector3.up;
	[SerializeField] private float _delayBetweenEpls = 0.2f;
	[Header("Run")]
	[SerializeField] private float _runSpeed;
	[SerializeField] private float _runTime;
	[Header("Ability")]
	private bool _isAlreadyAttack = false;

	private const GolemState GOLEM_STATE = GolemState.B;

	public override void Init()
	{
		base.Init();
		ResetAttack();
		currentAttackDelay = 0;
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
		if (controller.CurentState == GOLEM_STATE && !_isAlreadyAttack && !health.IsDead)
		{
			currentAttackDelay += Time.deltaTime;
			if (health.IsAbleToRebirth)
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
		health.Rebirth();
		if (!health.IsAbleToRebirth)
		{
			animator.SetTrigger(Const.AbilityName);
		}
	}

	private void StartRunning()
	{
		if (controller.CurentState == GOLEM_STATE)
		{
			_isAlreadyAttack = true;
			controller.SetSpeed(_runSpeed);
			animator.SetBool(Const.RunName, true);
			health.DisactiveHitAnim();
		}
	}

	private void StopRunning()
	{
		controller.ResetSpeed();

		if (controller.CurentState == GOLEM_STATE)
			animator.SetBool(Const.RunName, false);

		health.ActiveHitAnim();
	}

	private void InstExplosion(Vector3 pos, Quaternion quaternion, int damage)
	{
		GameObject gameObject = InstAttackObject(pos, quaternion);
		gameObject.TryGetComponent(out GolemExplosion explosion);
		explosion?.Init(damage);
	}

	private IEnumerator Attack()
	{
		StartRunning();

		yield return new WaitForSeconds(_runTime);

		if (controller.CurentState == GOLEM_STATE)
		{
			StopRunning();
			animator.SetTrigger(Const.Attack1Name);
		}
	}

	private IEnumerator AttackCoroutine()
	{
		Vector3 curentPos = transform.position;
		Vector3 dir = (player.transform.position - curentPos).normalized;
		for (int i = 0; i < _attackProjectilesCount; i++)
		{
			InstExplosion(curentPos + _offset * (i + 1) * dir + _startOffset, Quaternion.identity, _damage);
			yield return new WaitForSeconds(_delayBetweenEpls);
		}
	}
}