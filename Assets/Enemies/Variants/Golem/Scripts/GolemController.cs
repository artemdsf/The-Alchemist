using UnityEngine;

//Must be last in the hierarchy
[RequireComponent(typeof(GolemHealth), typeof(GolemAttack), typeof(GolemAttack))]
[RequireComponent(typeof(Animator))]
public class GolemController : EnemyController
{
	public Animator Animator { get; private set; }
	public GolemState CurentState { get; private set; }
	
	[SerializeField] private float _timeToChangeDir;
	[SerializeField] [Range(0, 1)] private float _wobbleRatio;
	[SerializeField] private float _lerpRatio;
	[SerializeField] private RuntimeAnimatorController[] animators;
	[SerializeField] private GolemAttackA _golemAttackA;
	[SerializeField] private GolemAttackB _golemAttackB;
	[SerializeField] private GolemAttackC _golemAttackC;
	[SerializeField] private GolemHealth _golemHealth;
	
	private Vector3 _lastDirection = Vector3.zero;
	private Vector3 _currentDirection;
	private float _currentTimeToChangeDir;
	private bool _isAbleToMove = true;

	public void InitBoss(ElementEnum element, Color color, int rebirthCount)
	{
		_golemHealth.SetRebirthCount(rebirthCount);
		SetElement(element, color);
	}

	public void ChangeState(GolemState state)
	{
		CurentState = state;
		Animator.runtimeAnimatorController = animators[(int)state];

		switch (state)
		{
			case GolemState.A:
				_golemAttackA.Init();
				break;
			case GolemState.B:
				_golemAttackB.Init();
				break;
			case GolemState.C:
				_golemAttackC.Init();
				break;
		}

		StartMove();
	}

	protected override void Awake()
	{
		base.Awake();

		_currentTimeToChangeDir = _timeToChangeDir;

		if (animators.Length != System.Enum.GetNames(typeof(GolemState)).Length)
		{
			Debug.LogError("Animators length not equal GolemAnimatorsEnum");
		}

		Animator = GetComponent<Animator>();
	}

	protected override void Update()
	{
		base.Update();

		if (isAlive)
		{
			_currentTimeToChangeDir += Time.deltaTime;
		}

		if (_currentTimeToChangeDir > _timeToChangeDir)
		{
			_currentTimeToChangeDir = 0;
			_lastDirection = _currentDirection;
			_currentDirection = GetRandomDirection();
		}
	}

	protected override void FixedUpdate()
	{
		if (isAlive && _isAbleToMove)
		{
			_lastDirection = Vector3.Lerp(_lastDirection, _currentDirection, Time.deltaTime * _lerpRatio);
			Move(_lastDirection);
		}
		else
		{
			Move(_currentDirection, 0);
		}
	}

	protected void StopMove()
	{
		_isAbleToMove = false;
	}

	protected void StartMove()
	{
		_isAbleToMove = true;
	}

	private void OnEnable()
	{
		ChangeState(GolemState.C);
	}

	private Vector3 GetRandomDirection()
	{
		float rot = Random.Range(0, 2 * Mathf.PI);
		Vector3 dir = new Vector3(Mathf.Sin(rot), Mathf.Cos(rot), 0) * _wobbleRatio;
		dir += (player.transform.position - transform.position).normalized;
		return dir.normalized;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + _currentDirection);
	}
}