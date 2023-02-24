using UnityEngine;

public class SlimeAttack : EnemyAttack
{
	[Header("Contact")]
	[SerializeField] private float _contactAttackDelay = 0.5f;
	[Header("Attack")]
	[SerializeField] private int _attackDamage = 5;
	[SerializeField] private float _attackDelay;
	[SerializeField] private float _attackRange;
	[SerializeField] private float _attackSpeed;
	[SerializeField] private ParticleSystem _particleSystem;
	[Header("Ability")]
	[SerializeField] private int _abilityDamage = 2;
	[SerializeField] private float _abilityDelay = 3;
	[SerializeField] private Vector2 _abilityRange = new Vector2(1, 2);
	[SerializeField] private float _abilityAttackDist = 1;
	[SerializeField] private GameObject _lightning;
	[SerializeField] private GameObject _lightningEnd;
	[Header("Additions")]
	[SerializeField] private SlimeController _controller;
	[SerializeField] private string _slimePoolName;

	public bool IsAlreadyAttack { get; private set; }

	public SlimeAttack TargetSlimeAtack;

	private Transform _pool;
	private float _currentAbilityDelay;
	private float _currentContactAttackDelay;
	private float _currentAttackDelay;
	private bool _isInitiator;

	public void StartAbility()
	{
		IsAlreadyAttack = true;
		animator.SetTrigger(Const.AbilityName);
		_currentAbilityDelay = 0;
	}

	private void EndAtack()
	{
		IsAlreadyAttack = false;
	}

	private void SpawnLightning()
	{
		if (_isInitiator && CheckLength(TargetSlimeAtack.transform.position, _abilityRange.x, _abilityRange.y))
		{
			_lightning.SetActive(true);
		}
		else
		{
			DespawnLightning();
		}
	}
	
	//Player P, Current transform C, Target T
	private void TryToDamage()
	{
		if (_isInitiator)
		{
			Vector3 CT = TargetSlimeAtack.transform.position - transform.position;
			Vector3 PT = Player.transform.position - transform.position;
			Vector3 PC = Player.transform.position - TargetSlimeAtack.transform.position;

			float dist = Vector3.Cross(PT, CT).magnitude / CT.magnitude;
			if (dist < _abilityAttackDist && CT.magnitude > PT.magnitude && CT.magnitude > PC.magnitude)
			{
				HitPlayer(_abilityDamage);
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

	private void Attack()
	{
		_particleSystem.Play();
		if (CheckLength(Player.transform.position, 0, _attackRange))
		{
			HitPlayer(_attackDamage);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == Const.PlayerName && _currentContactAttackDelay > _contactAttackDelay)
		{
			_currentContactAttackDelay = 0;
			HitPlayer();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		_pool = GameObject.Find(_slimePoolName).transform;
		SetParticleProperties();
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
			_currentAttackDelay += Time.deltaTime;
			_currentContactAttackDelay += Time.deltaTime;

			TryToAbility();
			TryToAttack();
		}
		else
		{
			if (_isInitiator)
			{
				_lightningEnd.transform.position = TargetSlimeAtack.transform.position + _controller.Pivot;
			}
		}
	}

	private void TryToAbility()
	{
		if (_currentAbilityDelay > _abilityDelay)
		{
			FindTarget();

			if (TargetSlimeAtack != null)
			{
				_isInitiator = true;

				TargetSlimeAtack.TargetSlimeAtack = this;
				StartAbility();
				TargetSlimeAtack.StartAbility();
			}
		}
	}

	private void TryToAttack()
	{
		if (_currentAttackDelay > _attackDelay)
		{
			if (CheckLength(Player.transform.position, 0, _attackRange))
			{
				IsAlreadyAttack = true;
				animator.SetTrigger(Const.Attack1Name);
				_currentAttackDelay = 0;
			}
		}
	}

	private void FindTarget()
	{
		foreach (Transform item in _pool)
		{
			if (CheckLength(item.position, _abilityRange.x, _abilityRange.y))
			{
				item.TryGetComponent(out TargetSlimeAtack);
				if (TargetSlimeAtack != null && !TargetSlimeAtack.IsAlreadyAttack)
				{
					return;
				}
			}
		}
	}

	private bool CheckLength(Vector3 pos, float min, float max)
	{
		float dist = (pos - transform.position).magnitude;
		return dist > min && dist < max;
	}

	private void SetParticleProperties()
	{
		_particleSystem.transform.position = _controller.Pivot + transform.position;
		ParticleSystem.MainModule main = _particleSystem.main;
		main.startSpeed = _attackSpeed;
		main.startLifetime = _attackRange / _attackSpeed;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(_controller.Pivot + transform.position, _abilityRange.x);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(_controller.Pivot + transform.position, _abilityRange.y);
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(_controller.Pivot + transform.position, _attackRange);
	}
}
