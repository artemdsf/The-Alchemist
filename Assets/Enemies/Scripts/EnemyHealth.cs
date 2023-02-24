using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] protected float maxHealth = 20;

	public bool IsDead { get; private set; }
	public bool IsHitAnimActive { get; private set; }

	protected float health;
	protected float armor;

	protected Animator animator;
	private Collider2D _collider;

	private ElementEnum _element;

	public virtual void TakeDamage(ElementEnum element, float dmg)
	{
		dmg *= GameManager.GetDamageMult(element, _element);

		if (armor > 0)
		{
			armor -= dmg;
			if (armor <= 0)
			{
				armor = 0;
				BreakArmor();
				DisactiveHitAnim();
			}
		}
		else
		{
			health -= dmg;
			if (health <= 0 && !IsDead)
			{
				health = 0;
				StartDeath();
			}
		}

		if (IsHitAnimActive)
		{
			animator.SetTrigger(Const.HitName);
		}
	}

	public void SetElement(ElementEnum element)
	{
		_element = element;
	}

	public void ActiveHitAnim()
	{
		IsHitAnimActive = true;
	}

	public void DisactiveHitAnim()
	{
		IsHitAnimActive = false;
	}

	protected void Death()
	{
		gameObject.SetActive(false);
	}

	protected void BreakArmor()
	{
		animator.SetTrigger(Const.BreakName);
	}

	protected virtual void OnEnable()
	{
		health = maxHealth;
		_collider.enabled = true;
		IsDead = false;
		ActiveHitAnim();
	}

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
		animator = GetComponent<Animator>();
	}

	private void StartDeath()
	{
		IsDead = true;
		_collider.enabled = false;
		animator.SetTrigger(Const.DeathName);
		animator.ResetTrigger(Const.RunName);
		DisactiveHitAnim();
	}
}