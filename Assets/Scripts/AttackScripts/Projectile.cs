using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AttackVisual))]
public class Projectile : Attack
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		TryDamage(collision);
		TryHeal(collision);
	}
}