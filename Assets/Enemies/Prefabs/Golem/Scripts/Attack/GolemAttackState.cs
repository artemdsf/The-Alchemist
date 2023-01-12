using UnityEngine;

public abstract class GolemAttackState : MonoBehaviour
{
	[Header("Projectile")]
	[SerializeField] protected string projectilesPoolName;

	[Header("Attack delay")]
	[SerializeField] protected float attackDelay = 1;
	protected float currentAttackDelay;

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

	protected GameObject InstAttackObject(Vector3 pos, Quaternion quaternion)
	{
		GameObject gameObject = _pool.GetPooledObject();
		gameObject.SetActive(true);
		gameObject.transform.position = pos;
		gameObject.transform.rotation = quaternion;
		return gameObject;
	}

	protected Vector3 InvertVectorByX(Vector3 vector)
	{
		return new Vector3(-vector.x, vector.y, vector.z);
	}
}