using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public bool IsDead => _isDead;

    [SerializeField] private float _maxHealth = 20;

	private EnemyVisual _enemyVisual;
	private Collider2D _collider;
	private Animator _animator;
	private ElementEnum _element;
	private Color _color;
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

	public void Init(ElementEnum element, Color color)
	{
		_element = element;
		_color = color;
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

	private void Start()
	{
		TryGetComponent(out _enemyVisual);
		_enemyVisual?.ChangeColor(_color);
	}

	private void StartDeath()
	{
		_isDead = true;
		_collider.enabled = false;
		_animator.SetTrigger("Death");
	}
}