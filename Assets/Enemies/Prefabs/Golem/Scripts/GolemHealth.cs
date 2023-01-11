using UnityEngine;

public class GolemHealth : EnemyHealth
{
	//To remove
	private void Start()
	{
		Debug.LogError("To remove");
		Init(ElementEnum.Earth);
	}

	public override void TakeDamage(ElementEnum element, float dmg)
	{
		base.TakeDamage(element, dmg);

		animator.SetTrigger("Hit");
	}
}
