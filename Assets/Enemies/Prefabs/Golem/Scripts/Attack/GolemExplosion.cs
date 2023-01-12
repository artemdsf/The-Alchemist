using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GolemExplosion : MonoBehaviour
{
	[SerializeField] private BoxCollider2D _boxCollider;
	[SerializeField] private uint _damage;

	private void Awake()
	{
		DisactiveCollider();
	}

	public void Init(uint damage)
	{
		_damage = damage;
	}

	public void ActiveCollider()
	{
		_boxCollider.enabled = true;
	}

	public void DisactiveCollider()
	{
		_boxCollider.enabled = false;
	}

	protected void Disactive()
	{
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerHealth health))
		{
			health.Damage(_damage);
		}
	}
}