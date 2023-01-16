using System.Collections;
using UnityEngine;

public class GolemAttackC : GolemAttackState
{
	[Header("Ability")]
	[SerializeField] private string _abilityName = "Ability";
	[Header("Attack1")]
	[SerializeField] private string _attack1Name = "Attack";
	[SerializeField] private uint _damage1 = 5;
	[SerializeField] private uint _attackThornsCount = 8;
	[SerializeField] private float _offset = 5;
	[SerializeField] private string _attack1PoolName;
	[Header("Attack2")]
	[SerializeField] private string _attack2Name = "Attack2";
	[SerializeField] private uint _damage2 = 5;
	[SerializeField] private uint _attackProjsCount = 4;
	[SerializeField] private float _startAngle = 45;
	[SerializeField] private Vector3 _firstPos;
	[SerializeField] private Vector3 _secondPos;
	[Header("Attack3")]
	[SerializeField] private float _slideSpeed = 5;
	[SerializeField] private float _slideLerpRatio = 1;
	[SerializeField] private string _attack3Name = "Attack3";
	[Header("Rebirth")]
	[SerializeField] private float _armor = 20;

	[SerializeField] private GolemHealth _health;

	private ObjectPool _attack1Pool;

	private const GolemState GOLEM_STATE = GolemState.C;

	private const int CIRCLE_DEGREES = 360;

	public override void Init()
	{
		base.Init();
		_health.AddArmor(_armor);
	}

	private void StartAttack1C()
	{
		Vector3 curentPos = transform.position;
		Vector3[] dirs = { Vector3.right, Vector3.left, Vector3.up, Vector3.down };

		foreach (var dir in dirs)
		{
			for (int i = 0; i < _attackThornsCount; i++)
			{
				InstThorns(curentPos + _offset * (i + 1) * dir, Quaternion.identity, _damage1);
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

	protected override void Start()
	{
		Debug.Log("To remove");
		base.Start();
		StartCoroutine(Tmp());
	}

	protected override void Awake()
	{
		base.Awake();

		_attack1Pool = GameObject.Find(_attack1PoolName)?.GetComponent<ObjectPool>();
	}

	private void Update()
	{

	}

	private void InstThorns(Vector3 pos, Quaternion quaternion, uint damage)
	{
		GameObject gameObject = InstAttackObject(pos, quaternion, _attack1Pool);
		gameObject.TryGetComponent(out GolemExplosion explosion);
		explosion?.Init(damage);
	}

	private void InstProjectie(Vector3 pos, Quaternion quaternion, uint damage)
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

	private IEnumerator Tmp()
	{
		while (true)
		{
			Attack2();
			yield return new WaitForSeconds(3);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position + _firstPos, "Center", false, Color.red);
		Gizmos.DrawIcon(transform.position + _secondPos, "Center", false, Color.green);
	}
}