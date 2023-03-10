using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class GolemProjectile : MonoBehaviour
{
	[SerializeField] private float _speed = 1f;
	[SerializeField] private float _lifeTime = 5f;
	private float _curentLifeTime = 0;
	private int _damage = 0;

	public void Init(int damage)
	{
		_damage = damage;
		_curentLifeTime = 0;
	}

	private void Update()
	{
		transform.position += transform.right * _speed * Time.deltaTime;
		_curentLifeTime += Time.deltaTime;

		if (_curentLifeTime > _lifeTime)
		{
			Disactive();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerHealth health))
		{
			health.Damage(_damage);
			Disactive();
		}
	}

	private void Disactive()
	{
		gameObject.SetActive(false);
	}
}
