using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 20;
    [SerializeField] private float _startArmor = 0;

	public bool IsDead { get; private set; }

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
			}
		}
		else
		{
			health -= dmg;
		}

		if (health <= 0 && !IsDead)
		{
			StartDeath();
		}
	}

	public void Init(ElementEnum element)
	{
		_element = element;
		health = maxHealth;
		armor = _startArmor;
		_collider.enabled = true;
		IsDead = false;
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

	private void BreakArmor()
	{
		animator.SetTrigger("Break");
	}

	private void StartDeath()
	{
		IsDead = true;
		_collider.enabled = false;
		animator.SetTrigger("Death");
	}
}