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

	private Vector3 _lastDirection = Vector3.zero;
	private Vector3 _curentDirection;
	private float _curentTimeToChangeDir;
	private bool _isAbleToMove = true;

	public void SetState(GolemState state)
	{
		CurentState = state;
		Animator.runtimeAnimatorController = animators[(int)state];
	}

	protected override void Awake()
	{
		base.Awake();

		_curentTimeToChangeDir = _timeToChangeDir;

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
			_curentTimeToChangeDir += Time.deltaTime;
		}

		if (_curentTimeToChangeDir > _timeToChangeDir)
		{
			_curentTimeToChangeDir = 0;
			_lastDirection = _curentDirection;
			_curentDirection = GetRandomDirection();
		}
	}

	protected override void FixedUpdate()
	{
		if (!GameManager.IsGamePaused && isAlive && _isAbleToMove)
		{
			_lastDirection = Vector3.Lerp(_lastDirection, _curentDirection, Time.deltaTime * _lerpRatio);
			Run(_lastDirection);
		}
		else
		{
			Run(_curentDirection, 0);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + _curentDirection);
	}

	private Vector3 GetRandomDirection()
	{
		float rot = Random.Range(0, 2 * Mathf.PI);
		Vector3 dir = new Vector3(Mathf.Sin(rot), Mathf.Cos(rot), 0) * _wobbleRatio;
		dir += (player.transform.position - transform.position).normalized;
		return dir.normalized;
	}
}