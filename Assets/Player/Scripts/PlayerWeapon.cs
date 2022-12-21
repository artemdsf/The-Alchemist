using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField] private float _damage = 10;
	[SerializeField] private int _heal = 1;
	public ElementsManager ElementsManager;

	[Header("Weapons")]
	[SerializeField] private Weapon[] _weapons;

	private Weapon _curentWeapon;

	public float Damage => _damage;
	public int Heal => _heal;
	private ElementEnum _element;
	private int _elementsCount;

	private void Start()
	{
		_elementsCount = System.Enum.GetNames(typeof(ElementEnum)).Length;
		SelectElement(0);

		if (_weapons.Length != _elementsCount)
		{
			throw new System.Exception("FireWeapons count not equal ElementEnum count");
		}
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			TryAttack();
		}

		for (int i = 0; i < _elementsCount; i++)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1 + i))
			{
				SelectElement(i);
			}
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			SelectElement((int)_element + 1);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			SelectElement((int)_element - 1);
		}
	}

	private void TryAttack()
	{
		Vector3 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 delta = mousePos - transform.position;
		float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		_curentWeapon.TryAttack(transform.position, Quaternion.Euler(0, 0, angle));
	}

	private void SelectElement(int element)
	{
		if (element < 0)
		{
			element += _elementsCount;
		}
		element %= _elementsCount;

		_element = (ElementEnum)element;
		ElementsManager.SelectElement(element);
		_curentWeapon = _weapons[element];
	}
}