public class PebbleHealth : EnemyHealth
{
	protected override void OnEnable()
	{
		base.OnEnable();
		animator.SetTrigger(Const.RunName);
	}
}