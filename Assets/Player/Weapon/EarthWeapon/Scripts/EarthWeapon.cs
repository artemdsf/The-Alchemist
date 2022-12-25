using UnityEngine;

public class EarthWeapon : Weapon
{
	[Header("Projectiles Count")]
	[SerializeField] private int _rocksCont = 1;

	private const int CIRCLE_DEGREES = 360;

	protected override void Attack(Vector3 pos, Quaternion quaternion)
	{
		for (int i = 0; i < _rocksCont; i++)
		{
			Quaternion tmp = Quaternion.Euler(Vector3.forward * CIRCLE_DEGREES * i / _rocksCont);
			GameObject newObject = _pool.GetPooledObject();
			newObject.SetActive(true);
			newObject.transform.position = pos;
			newObject.transform.rotation = tmp;
			newObject.TryGetComponent(out PlayerProjectile newProjectile);
			InitAttack(newProjectile);
		}
	}
}