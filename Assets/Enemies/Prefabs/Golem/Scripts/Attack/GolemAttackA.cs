using UnityEngine;

public class GolemAttackA : GolemAttackState
{
	[Header("Attack 2")]
	[SerializeField] private Vector3 _attack2Pos;
	[SerializeField] private uint _damage2 = 5;
	[SerializeField] private uint _attack2ProjectilesCount = 8;
	[SerializeField] private string _attack2Name = "Attack 2";
	[Header("Attack 3")]
	[SerializeField] private Vector3 _attack3Pos;
	[SerializeField] private uint _damage3 = 5;
	[SerializeField] private uint _attack3ProjectilesCount = 4;
	[SerializeField] [Min(0)] private float _attack3MaxAngle = 30f;
	[SerializeField] private string _attack3Name = "Attack 3";

	private const int CIRCLE_DEGREES = 360;

	//Circular attack
	protected void Attack2A()
	{
		float startAngle = Random.Range(0, CIRCLE_DEGREES);
		for (int i = 0; i < _attack2ProjectilesCount; i++)
		{
			Quaternion quaternion = Quaternion.Euler(Vector3.forward * (CIRCLE_DEGREES * i / _attack2ProjectilesCount + startAngle));

			if (controller.IsOrientRight)
				InstProjectile(transform.position + _attack2Pos, quaternion, _damage2);
			else
				InstProjectile(transform.position + InvertVectorByX(_attack2Pos), quaternion, _damage2);
		}
	}

	//Directed attack
	protected void Attack3A()
	{
		for (int i = 0; i < _attack3ProjectilesCount; i++)
		{
			float angleDelta;
			Quaternion quaternion = Quaternion.identity;

			angleDelta = _attack3MaxAngle * 2 / (_attack3ProjectilesCount - 1);

			if (controller.IsOrientRight)
			{
				if (_attack3ProjectilesCount > 1)
					quaternion = Quaternion.Euler(Vector3.forward * (angleDelta * i - _attack3MaxAngle));
				InstProjectile(transform.position + _attack3Pos, quaternion, _damage3);
			}
			else
			{
				if (_attack3ProjectilesCount > 1)
					quaternion = Quaternion.Euler(Vector3.forward * (angleDelta * i - _attack3MaxAngle - CIRCLE_DEGREES / 2));
				InstProjectile(transform.position + InvertVectorByX(_attack3Pos), quaternion, _damage3);
			}
		}
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused && controller.CurentState == GolemState.A)
		{
			attackDelay += Time.deltaTime;

			if (attackDelay > maxAttackDelay)
			{
				Attack();
				attackDelay = 0;
			}
		}
	}

	public void Attack()
	{
		Vector3 attackDir = player.transform.position - transform.position;
		float angle;
		if (controller.IsOrientRight)
			angle = Vector3.Angle(Vector3.right, attackDir);
		else
			angle = Vector3.Angle(Vector3.left, attackDir);

		if (angle > _attack3MaxAngle)
		{
			animator.SetTrigger(_attack2Name);
			animator.ResetTrigger(_attack3Name);
		}
		else
		{
			animator.SetTrigger(_attack3Name);
			animator.ResetTrigger(_attack2Name);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position + _attack2Pos, "Center", false, Color.red);
		Gizmos.DrawIcon(transform.position + _attack3Pos, "Center", false, Color.green);
	}
}