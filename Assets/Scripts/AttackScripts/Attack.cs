using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AttackVisual), typeof(PlayerHealth), typeof(Collider2D))]
public class Attack : MonoBehaviour
{
	[Header("Properties")]
	[SerializeField] private float _damage = 0;
	[SerializeField] private int _heal = 0;

	private EnemyHealth _enemyHealth;
	private PlayerHealth _playerHealth;
	private Collider2D _collider;
	private AttackVisual _ptojectileVisual;

	private bool _canBeDestroyed = true;

	private float _speed = 0;
	private float _rotationSpeed = 0;

	private float _lifeTime = 0;
	private float _curentLifeTime = 0;

	public void Init(float speed, float rotationSpeed, float lifeTime, bool canBeDestroyed)
	{	
		_curentLifeTime = 0;
		_speed = speed;
		_rotationSpeed = rotationSpeed;
		_canBeDestroyed = canBeDestroyed;
		_lifeTime = lifeTime;
		_collider.enabled = true;
		_ptojectileVisual.Init();
	}

	protected virtual void Disactivate()
	{
		StartCoroutine(Death());
	}

	protected virtual void Awake()
	{
		_ptojectileVisual = GetComponent<AttackVisual>();
		_playerHealth = FindObjectOfType<PlayerHealth>();
		_collider = GetComponent<Collider2D>();
	}

	protected virtual void Update()
	{
		Move();
		Rotate();
		CheckForLifeTime();
	}

	protected void TryDamage(Collider2D collider)
	{
		if (_damage > 0 && collider.tag == "Enemy")
		{
			Damage(collider);

			if (_canBeDestroyed)
			{
				Disactivate();
			}
		}
	}

	protected virtual void TryHeal(Collider2D collider)
	{
		if (_heal > 0 && collider.tag == "Player")
		{
			Heal(collider);

			if (_canBeDestroyed)
			{
				Disactivate();
			}
		}
	}

	private void Move()
	{
		transform.position += transform.right * _speed * Time.deltaTime;
	}

	private void Rotate()
	{
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
			Vector3.forward * _rotationSpeed * Time.deltaTime);
	}

	protected virtual void Damage(Collider2D collision)
	{
		collision.TryGetComponent(out _enemyHealth);
		_enemyHealth?.TakeDamage(_damage);
		_enemyHealth = null;
	}

	protected virtual void Heal(Collider2D collision)
	{
		_playerHealth?.Heal(_heal);
	}

	private void CheckForLifeTime()
	{
		_curentLifeTime += Time.deltaTime;
		if (_curentLifeTime > _lifeTime)
		{
			Disactivate();
		}
	}

	private IEnumerator Death()
	{
		_collider.enabled = false;
		_speed = 0;
		_rotationSpeed = 0;

		if (_ptojectileVisual.SaveParticles)
		{
			_ptojectileVisual.Death();

			yield return new WaitForSeconds(_ptojectileVisual.SafeTime);
		}

		gameObject.SetActive(false);
	}
}
