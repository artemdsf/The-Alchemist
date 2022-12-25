using System;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
	public float Damage => _damage;
	public int Heal => _heal;

	public ElementsManagerUI ElementsManager;

	[SerializeField] private float _damage = 10;
	[SerializeField] private int _heal = 1;

	[Header("Weapons")]
	[SerializeField] private Weapon[] _weapons;

	private Weapon _curentWeapon;
	private ElementEnum _element;
	private int _elementsCount;

	private void Start()
	{
		foreach (var item in _weapons)
		{
			item.Init(ElementsManager);
		}

		_elementsCount = Enum.GetNames(typeof(ElementEnum)).Length;
		SelectElement(0);

		if (_weapons.Length != _elementsCount)
		{
			throw new Exception("FireWeapons count not equal ElementEnum count");
		}
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused)
		{
			CheckElement();
			CheckAttack();
		}
	}

	private void CheckElement()
	{
		for (int i = 0; i < _elementsCount; i++)
		{
			if (Input.GetKeyDown(HotKeys.Elements[i]))
			{
				SelectElement(i);
			}
		}

		int shift = Math.Sign(Input.GetAxis("Mouse ScrollWheel")) * HotKeys.InvertMouseWheel;
		SelectElement((int)_element + shift);
	}

	private void CheckAttack()
	{
		if (Input.GetKey(HotKeys.Attack))
		{
			TryAttack();
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