using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(EnemyController))]
public class EnemySpawner : MonoBehaviour
{
	[SerializeField] protected ElementEnum element;
	[SerializeField] protected Color color = Color.white;
	[SerializeField] private float _spawnTime = 1;
	[SerializeField] private float _spawnRangeMult = 2.3f;
	[SerializeField] private Color _gizmosColor;

	[SerializeField] protected GameObject player;
	private EnemyController _enemyController;
	private ObjectPool _pool;

	protected float spawnRange;
	private float _curentTime;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		_enemyController = GetComponent<EnemyController>();
		spawnRange = Camera.main.orthographicSize * _spawnRangeMult;
	}

	public void Init(ElementEnum element, Color color, ObjectPool pool)
	{
		this.element = element;
		this.color = color;
		_pool = pool;

		_curentTime = 0;

		_enemyController.Init(this.element, this.color);
	}

	protected virtual void Update()
	{
		TrySpawnEnemy();
	}

	private void TrySpawnEnemy()
	{
		_curentTime += Time.deltaTime;
		if (_curentTime > _spawnTime)
		{
			SpawnEnemy();
			_curentTime = 0;
		}
	}

	private void SpawnEnemy()
	{
		Vector3 distVector = Random.insideUnitCircle.normalized * spawnRange;
		Vector3 pos = player.transform.position + distVector;

		GameObject gameObject = _pool.GetPooledObject();
		gameObject.SetActive(true);
		gameObject.transform.position = pos;
		gameObject.TryGetComponent(out EnemyController enemyController);
		enemyController.Init(element, color);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = _gizmosColor;
		float _spawnRange = Camera.main.orthographicSize * _spawnRangeMult;
		Gizmos.DrawWireSphere(player.transform.position, _spawnRange);
	}
}