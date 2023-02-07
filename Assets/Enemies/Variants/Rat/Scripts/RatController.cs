using UnityEngine;

[RequireComponent(typeof(RatAttack), typeof(RatHealth))]
public class RatController : EnemyController
{
	public Animator Animator { get; private set; }

	private RatAttack _attack;

	protected override void Awake()
	{
		base.Awake();
		Animator = GetComponent<Animator>();
		_attack = GetComponent<RatAttack>();
	}
}
