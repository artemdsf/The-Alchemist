using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttack : EnemyAttack
{
	[Header("Abiltity")]
	[SerializeField] private string _abilityName = "Ability";
	[SerializeField] private string _runName = "Run";
	[SerializeField] private float _runSpeed = 5;
	[SerializeField] private float _runTime = 5;
	[SerializeField] private Vector2 _delayRange;
	[Header("Attack")]
	[SerializeField] private float _attackDelay = 0.5f;

	private float _currentAttackDelay;
	private bool _isRunning = false;

	private RatController _controller;

	protected override void Awake()
	{
		base.Awake();
		_controller = GetComponent<RatController>();
	}

	private void Update()
	{
		_currentAttackDelay += Time.deltaTime;
	}

	private void OnEnable()
	{
		Init();
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (_currentAttackDelay > _attackDelay && collision.gameObject.tag == Const.PlayerName)
		{
			HitPlayer();
			_currentAttackDelay = 0;
		}
	}

	private void Init()
	{
		_isRunning = false;
		animator.SetBool(_runName, false);
		StartCoroutine(Cycle());
	}

	private void StartAbility()
	{
		_isRunning = true;
		animator.SetTrigger(_abilityName);
	}

	private IEnumerator Ability()
	{
		_controller.SetSpeed(_runSpeed);
		animator.SetBool(_runName, true);

		yield return new WaitForSeconds(_runTime);

		_controller.ResetSpeed();
		animator.SetBool(_runName, false);
		_isRunning = false;
	}

	private IEnumerator Cycle()
	{
		while (true)
		{
			if (!_isRunning)
			{
				float delay = Random.Range(_delayRange.x, _delayRange.y);
				yield return new WaitForSeconds(delay);
				StartAbility();
			}
			else
			{
				yield return null;
			}
		}
	}
}
