using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[Header("Element")]
	[SerializeField] private ElementEnum _elementEnum;

	[Header("Properties")]
    [SerializeField] private float _delay;
	[SerializeField] private float _damage = 0;
	[SerializeField] private int _heal = 0;

	[Header("Projectile")]
	[SerializeField] private float _speed = 5;
	[SerializeField] private float _rotationSpeed = 5;
	[SerializeField] private float _lifeTime = 3;
	[SerializeField] private bool _canBeDestroyerd = true;

	[Header("Pool")]
	[SerializeField] protected ObjectPool _pool;

	private float _curectDelay;
	private ElementsManagerUI _elementsManager;

	public void Init(ElementsManagerUI elementsManager)
	{
		_elementsManager = elementsManager;
	}

	public void TryAttack(Vector3 pos, Quaternion quaternion)
	{
		if (_curectDelay >= _delay)
		{
			Attack(pos, quaternion);
			_curectDelay = 0;
		}
	}

	protected virtual void Attack(Vector3 pos, Quaternion quaternion)
	{
		GameObject newObject = _pool.GetPooledObject();
		newObject.SetActive(true);
		newObject.transform.position = pos;
		newObject.transform.rotation = quaternion;
		newObject.TryGetComponent(out PlayerAttack attack);
		InitAttack(attack);
	}

	protected void InitAttack(PlayerAttack attack)
	{
		attack?.Init(_damage, _heal, _speed, _rotationSpeed, 
			_lifeTime, _canBeDestroyerd, _elementEnum);
	}

	private void Start()
	{
		_curectDelay = _delay;
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused)
		{
			UpdateDelay();
		}
	}

	private void UpdateDelay()
	{
		_curectDelay += Time.deltaTime;

		if (_curectDelay > _delay)
		{
			_curectDelay = _delay;
		}
		_elementsManager?.SetReloadBar((int)_elementEnum, _curectDelay / _delay);
	}
}