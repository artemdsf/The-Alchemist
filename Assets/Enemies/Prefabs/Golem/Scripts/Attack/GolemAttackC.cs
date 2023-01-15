using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackC : GolemAttackState
{
	[Header("Rebirth")]
	[SerializeField] private float _armor = 20;

	[SerializeField] private GolemHealth _health;
	
	private const GolemState GOLEM_STATE = GolemState.C;

	public override void Init()
	{
		base.Init();
		_health.AddArmor(_armor);
	}
}
