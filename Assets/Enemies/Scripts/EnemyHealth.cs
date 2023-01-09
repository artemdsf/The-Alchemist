using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public bool IsDead => _isDead;

    [SerializeField] private float _maxHealth = 20;

	private Collider2D _collider;
	private Animator _animator;

	private ElementEnum _element;
	
	private float _health;
	private bool _isDead;

	public void TakeDamage(ElementEnum element, float dmg)
	{
		_health -= dmg * GameManager.GetDamageMult(element, _element);

		if (_health <= 0 && !_isDead)
		{
			StartDeath();
		}
	}

	public void Init(ElementEnum element)
	{
		_element = element;
		_health = _maxHealth;
		_collider.enabled = true;
		_isDead = false;
	}

	protected void Death()
	{
		gameObject.SetActive(false);
	}

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
		_animator = GetComponent<Animator>();
	}

	private void StartDeath()
	{
		_isDead = true;
		_collider.enabled = false;
		_animator.SetTrigger("Death");
	}
}