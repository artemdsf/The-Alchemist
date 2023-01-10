using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHealth : EnemyHealth
{
	public override void TakeDamage(ElementEnum element, float dmg)
	{
		base.TakeDamage(element, dmg);

		animator.SetTrigger("Hit");
	}
}
