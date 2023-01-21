public class PebbleHealth : EnemyHealth
{
	public override void Init(ElementEnum element)
	{
		base.Init(element);

		animator.SetTrigger("Run");
	}
}