using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(GolemHealth), typeof(GolemAttack), typeof(GolemAttack))]
[RequireComponent(typeof(Animator))]
public class GolemController : EnemyController
{
	[SerializeField] private float _timeToChangeDir;
	[SerializeField] [Range(0, 1)] private float _wobbleRatio;
	[SerializeField] private float _lerpRatio;
	[SerializeField] private AnimatorController[] animators;
	public Animator Animator { get; private set; }
	public GolemState CurentState { get; private set; }
	public GolemState MaxState { get; private set; }
	public int RebirthsCount { get; private set; }

	[SerializeField] private GolemAttackA _golemAttackA;
	[SerializeField] private GolemAttackB _golemAttackB;
	[SerializeField] private GolemAttackC _golemAttackC;

	[SerializeField] private GolemHealth _golemHealth;
	private Vector3 _lastDirection = Vector3.zero;
	private Vector3 _currentDirection;
	private float _currentTimeToChangeDir;
	private bool _isAbleToMove = true;

	public void Init(GolemState maxState, int rebirthCount)
	{
		RebirthsCount = rebirthCount;
		MaxState = maxState;
		_golemHealth.Init(rebirthCount);
		ChangeState(maxState);
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
	}

	protected override void Start()
	{
		base.Start();

		Debug.LogError("To remove");
		Init(GolemState.C, 2);
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

		if (!GameManager.IsGamePaused && isAlive)
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
		if (!GameManager.IsGamePaused && isAlive && _isAbleToMove)
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

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + _currentDirection);
	}

	private Vector3 GetRandomDirection()
	{
		float rot = Random.Range(0, 2 * Mathf.PI);
		Vector3 dir = new Vector3(Mathf.Sin(rot), Mathf.Cos(rot), 0) * _wobbleRatio;
		dir += (player.transform.position - transform.position).normalized;
		return dir.normalized;
	}
}