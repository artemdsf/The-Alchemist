using UnityEngine;

public class GolemHealth : EnemyHealth
{
	public bool IsAbleToRebirth => health < maxHealth / (_maxRebirthCount + 1) * _rebirthCountLeft && health > 0;

	public bool IsImmuneToDamage { get; private set; }

	private int _maxRebirthCount = 0;
	private int _rebirthCountLeft = 0;

	public void SetRebirthCount(int rebirthCount)
	{
		_maxRebirthCount = rebirthCount;
		_rebirthCountLeft = rebirthCount;
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

	protected override void OnEnable()
	{
		base.OnEnable();
		DisactiveImmuneToDamage();
	}
}