using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : EnemyAttack
{
	[Header("Ability")]
	[SerializeField] private float _abilityDelay = 3;
	[SerializeField] private Vector2 _abilityRange = new Vector2(1, 2);
	[SerializeField] private float _abilityAttackDist = 1;
	[SerializeField] private GameObject _lightning;
	[SerializeField] private GameObject _lightningEnd;
	[Header("Additions")]
	[SerializeField] private SlimeController _controller;
	[SerializeField] private string _slimePoolName;

	public bool IsAlreadyAttack { get; private set; }

	private Transform _pool;
	private SlimeAttack _targetSlimeAtack;
	private float _currentAbilityDelay;
	private bool _isInitiator;

	public void StartAttack()
	{
		IsAlreadyAttack = true;
		animator.SetTrigger(Const.AbilityName);
		_currentAbilityDelay = 0;

	}

	private void SpawnLightning()
	{
		if (_isInitiator)
		{
			_lightning.SetActive(true);
		}
	}

	//Player P, Current transform C, Target T
	private void TryToDamage()
	{
		if (_isInitiator)
		{
			Vector3 CT = _targetSlimeAtack.transform.position - transform.position;
			Vector3 PT = Player.transform.position - transform.position;
			Vector3 PC = Player.transform.position - _targetSlimeAtack.transform.position;

			float dist = Vector3.Cross(PT, CT).magnitude / CT.magnitude;
			if (dist < _abilityAttackDist && CT.magnitude > PT.magnitude && CT.magnitude > PC.magnitude)
			{
				HitPlayer();
			}
		}
	}

	private void DespawnLightning()
	{
		_isInitiator = false;
		IsAlreadyAttack = false;
		_lightning.SetActive(false);
		_lightningEnd.transform.position = transform.position + _controller.Pivot;
	}

	protected override void Awake()
	{
		base.Awake();
		_pool = GameObject.Find(_slimePoolName).transform;
	}

	private void OnEnable()
	{
		_isInitiator = false;
		_currentAbilityDelay = 0;
		_lightning.SetActive(false);
	}

	private void Update()
	{
		if (!IsAlreadyAttack)
		{
			_currentAbilityDelay += Time.deltaTime;

			TryToAbility();
		}
		else
		{
			if (_isInitiator)
			{
				_lightningEnd.transform.position = _targetSlimeAtack.transform.position + _controller.Pivot;
			}
		}
	}

	private void TryToAbility()
	{
		if (_currentAbilityDelay > _abilityDelay)
		{
			FindTarget();

			if (_targetSlimeAtack != null)
			{
				_isInitiator = true;

				StartAttack();
				_targetSlimeAtack.StartAttack();
			}
		}
	}

	private void FindTarget()
	{
		foreach (Transform item in _pool)
		{
			float dist = (item.position - transform.position).magnitude;
			if (dist > _abilityRange.x && dist < _abilityRange.y)
			{
				item.TryGetComponent(out _targetSlimeAtack);
				if (_targetSlimeAtack != null && !_targetSlimeAtack.IsAlreadyAttack)
				{
					return;
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(_controller.Pivot + transform.position, _abilityRange.x);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(_controller.Pivot + transform.position, _abilityRange.y);
	}
}
