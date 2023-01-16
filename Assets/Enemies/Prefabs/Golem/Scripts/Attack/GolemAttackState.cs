using UnityEngine;

public abstract class GolemAttackState : MonoBehaviour
{
	[Header("Attack")]
	[SerializeField] protected string attackPoolName;

	[Header("Attack delay")]
	[SerializeField] protected float attackDelay = 1;
	protected float currentAttackDelay;

	protected GolemController controller;
	protected Animator animator;
	protected GameObject player;

	private ObjectPool _pool;

	public virtual void Init()
	{
		currentAttackDelay = 0;
	}

	protected virtual void Awake()
	{
		controller = GetComponent<GolemController>();
		_pool = GameObject.Find(attackPoolName)?.GetComponent<ObjectPool>();
	}

	protected virtual void Start()
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

	protected GameObject InstAttackObject(Vector3 pos, Quaternion quaternion, ObjectPool pool)
	{
		GameObject gameObject = pool.GetPooledObject();
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