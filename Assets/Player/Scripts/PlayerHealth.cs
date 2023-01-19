using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int MaxHealth => _maxHealth;
	public int Health => _health;

	[SerializeField] private int _maxHealth = 100;
	private int _health = 100;
	private int _defense = 0;

	private void Awake()
	{
		_health = _maxHealth;
	}

	public void Damage(int attack)
	{
		int _damage = attack - _defense >= 0 ? attack - _defense : 0;

		_health -= _damage;

		if (_health <= 0)
		{
			_health = 0;
			Death();
		}
	}

	public void Heal(int heal)
	{
		_health += heal;

		if (_health >= _maxHealth)
		{
			_health = _maxHealth;
		}
	}

	private void Death()
	{
		Debug.Log("Death");
	}
}
