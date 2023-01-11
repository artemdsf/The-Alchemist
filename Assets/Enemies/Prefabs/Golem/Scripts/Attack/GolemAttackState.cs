using UnityEngine;

public class GolemAttackState : MonoBehaviour
{
	[Header("Projectile")]
	[SerializeField] protected string projectilesPoolName;

	[Header("Attack delay")]
	[SerializeField] protected float maxAttackDelay = 1;
	protected float attackDelay;

	protected GolemController controller;
	protected Animator animator;
	protected GameObject player;

	private ObjectPool _pool;

	private void Awake()
	{
		controller = GetComponent<GolemController>();
		_pool = GameObject.Find(projectilesPoolName)?.GetComponent<ObjectPool>();
	}

	private void Start()
	{
		player = GetComponent<GolemAttack>().Player;
		animator = controller.Animator;
	}

	protected void InstProjectile(Vector3 pos, Quaternion quaternion, uint damage)
	{
		GameObject gameObject = _pool.GetPooledObject();
		gameObject.SetActive(true);
		gameObject.transform.position = pos;
		gameObject.transform.rotation = quaternion;
		gameObject.TryGetComponent(out GolemProjectile projectile);
		projectile?.Init(damage);
	}

	protected Vector3 InvertVectorByX(Vector3 vector)
	{
		return new Vector3(-vector.x, vector.y, vector.z);
	}
}