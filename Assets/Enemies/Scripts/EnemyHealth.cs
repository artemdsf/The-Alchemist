using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private ElementEnum _element;
    [SerializeField] private float _maxHealth = 20;
    private float _health;

	private void Awake()
	{
		_health = _maxHealth;
	}

	public void TakeDamage(ElementEnum element, float dmg)
	{
		_health -= dmg * GameManager.GetDamageMult(element, _element);

		if (_health <= 0)
		{
			Death();
		}
	}

	private void Death()
	{
		gameObject.SetActive(false);
	}
}
