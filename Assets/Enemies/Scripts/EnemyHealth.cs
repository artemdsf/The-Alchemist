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
			animator.SetTrigger("Hit");
		}
	}

	public virtual void Init(ElementEnum element)
	{
		IsHitAnimActive = true;
		_element = element;
		health = maxHealth;
		_collider.enabled = true;
		IsDead = false;
		ActiveHitAnim();
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

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
		animator = GetComponent<Animator>();
	}

	protected void BreakArmor()
	{
		animator.SetTrigger("Break");
	}

	private void StartDeath()
	{
		IsDead = true;
		_collider.enabled = false;
		animator.SetTrigger("Death");
		animator.ResetTrigger("Run");
		DisactiveHitAnim();
	}
}