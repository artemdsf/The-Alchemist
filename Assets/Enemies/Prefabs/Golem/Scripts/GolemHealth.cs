using UnityEngine;

public class GolemHealth : EnemyHealth
{
	public bool IsAbleToRebirth => health < maxHealth / (_maxRebirthCount + 1) * _rebirthCountLeft;
	public bool IsHitAnimActive { get; private set; }

	private int _maxRebirthCount;
	private int _rebirthCountLeft;

	public void Init(int rebirthCount)
	{
		_maxRebirthCount = rebirthCount;
		_rebirthCountLeft = rebirthCount;
		ActiveHitAnim();
	}

	public override void TakeDamage(ElementEnum element, float dmg)
	{
		if (IsHitAnimActive)
		{
			animator.SetTrigger("Hit");
		}

		base.TakeDamage(element, dmg);
	}

	public void AddArmor(float armor)
	{
		this.armor += armor;
	}

	public void Rebirth()
	{
		_rebirthCountLeft--;
	}

	public void DisactiveHitAnim()
	{
		IsHitAnimActive = false;
	}

	public void ActiveHitAnim()
	{
		IsHitAnimActive = true;
	}

	//To remove
	private void Start()
	{
		Debug.LogError("To remove");
		Init(ElementEnum.Earth);
	}
}