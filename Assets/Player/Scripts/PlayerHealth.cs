using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public uint MaxHealth => _maxHealth;
	public uint Health => _health;

	[SerializeField] private uint _maxHealth = 100;
	private uint _health = 100;
	private uint _defense = 0;

	private void Awake()
	{
		_health = _maxHealth;
	}

	public void Damage(uint attack)
	{
		uint _damage = attack - _defense >= 0 ? attack - _defense : 0;

		_health -= _damage;

		if (_health <= 0)
		{
			_health = 0;
			Death();
		}
	}

	public void Heal(uint heal)
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
