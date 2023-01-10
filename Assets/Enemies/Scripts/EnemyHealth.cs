using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 20;
	
	public bool IsDead { get; private set; }

	protected Animator animator;
	private Collider2D _collider;

	private ElementEnum _element;
	
	private float _health;

	public virtual void TakeDamage(ElementEnum element, float dmg)
	{
		_health -= dmg * GameManager.GetDamageMult(element, _element);

		if (_health <= 0 && !IsDead)
		{
			StartDeath();
		}
	}

	public void Init(ElementEnum element)
	{
		_element = element;
		_health = _maxHealth;
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

	private void StartDeath()
	{
		IsDead = true;
		_collider.enabled = false;
		animator.SetTrigger("Death");
	}
}