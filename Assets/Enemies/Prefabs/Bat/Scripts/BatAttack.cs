using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAttack : EnemyAttack
{
	[SerializeField] private Vector2 _attackOffset;
	[SerializeField] private float _attackRange;
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (GameManager.IsGamePaused)
		{
			_animator.speed = 0;
		}
		else
		{
			_animator.speed = 1;
			if (CheckRange())
			{
				StartAttack();
			}
			else
			{
				EndAttack();
			}
		}
	}

	private void StartAttack()
	{
		_animator.SetTrigger("Attack");
	}

	private void EndAttack()
	{
		_animator.ResetTrigger("Attack");
	}

	protected void TryToHit()
	{
		if (CheckRange())
		{
			Hit();
		}
	}

	private bool CheckRange()
	{
		return (_player.transform.position - (transform.position + (Vector3)_attackOffset)).magnitude < _attackRange;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position + (Vector3)_attackOffset, _attackRange);
	}
}