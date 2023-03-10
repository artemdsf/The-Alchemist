using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AttackVisual))]
public class PlayerProjectile : PlayerAttack
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		TryDamage(collision);
		TryHealPlayer(collision);
	}
}