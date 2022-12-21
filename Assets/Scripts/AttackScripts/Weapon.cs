using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected ElementEnum elementEnum;
    [SerializeField] protected float delay;
    protected float curectDelay;

	[Header("Projectile")]
	[SerializeField] private float _speed = 5;
	[SerializeField] private float _rotationSpeed = 5;
	[SerializeField] private float _lifeTime = 3;
	[SerializeField] private bool _canBeDestroyerd = true;

	[Header("Pool")]
	[SerializeField] protected ObjectPool _pool;

	private PlayerWeapon _playerWeapon;
    private ElementsManager _elementsManager;

	public void TryAttack(Vector3 pos, Quaternion quaternion)
	{
		if (curectDelay >= delay)
		{
			Attack(pos, quaternion);
			curectDelay = 0;
		}
	}

	protected virtual void Start()
	{
		if (transform.parent.TryGetComponent(out _playerWeapon))
			_elementsManager = _playerWeapon.ElementsManager;
		curectDelay = delay;
	}

	protected virtual void Update()
	{
		curectDelay += Time.deltaTime;

		if (curectDelay > delay)
		{
			curectDelay = delay;
		}

		_elementsManager?.SetReloadBar((int)elementEnum, curectDelay / delay);
	}

	protected virtual void Attack(Vector3 pos, Quaternion quaternion)
	{
		GameObject newObject = _pool.GetPooledObject();
		newObject.SetActive(true);
		newObject.transform.position = pos;
		newObject.transform.rotation = quaternion;
		newObject.TryGetComponent(out Attack attack);
		Init(attack);
	}

	protected void Init(Attack attack)
	{
		attack?.Init(_speed, _rotationSpeed, _lifeTime, _canBeDestroyerd);
	}
}