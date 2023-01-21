using UnityEngine;

public class GolemHealth : EnemyHealth
{
	public bool IsAbleToRebirth => health < maxHealth / (_maxRebirthCount + 1) * _rebirthCountLeft && health > 0;

	public bool IsImmuneToDamage { get; private set; }

	private int _maxRebirthCount;
	private int _rebirthCountLeft;

	public void Init(int rebirthCount)
	{
		_maxRebirthCount = rebirthCount;
		_rebirthCountLeft = rebirthCount;
		DisactiveImmuneToDamage();
	}

	public override void TakeDamage(ElementEnum element, float dmg)
	{
		if (!IsImmuneToDamage && !IsDead)
		{
			base.TakeDamage(element, dmg);

		}
	}

	public void AddArmor(float armor)
	{
		this.armor += armor;
	}

	public void Rebirth()
	{
		_rebirthCountLeft--;
	}

	public void ActiveImmuneToDamage()
	{
		IsImmuneToDamage = true;
	}

	public void DisactiveImmuneToDamage()
	{
		IsImmuneToDamage = false;
	}
}