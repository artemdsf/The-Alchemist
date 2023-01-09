using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ProjectileController : MonoBehaviour
{
	[SerializeField] private float _speed = 1f;
	[SerializeField] private float _lifeTime = 5f;
	private float _curentLifeTime = 0;
	private uint _damage = 0;

	public void Init(uint damage)
	{
		_damage = damage;
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
		Destroy(gameObject);
	}
}
