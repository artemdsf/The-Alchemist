using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(EnemyController))]
public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private string _bossPoolName;
	[SerializeField] private ElementEnum _element;
	[SerializeField] private Color _color;
	[SerializeField] private float _spawnTime = 1;
	[SerializeField] private float _spawnRangeMult = 2.3f;
	[SerializeField] private Color _gizmosColor;

	[SerializeField] private GameObject _player;
	private EnemyController _enemyController;
	private ObjectPool _pool;
	private ObjectPool _bossPool;

	private float _spawnRange;
	private float _curentTime;

	private void Awake()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_enemyController = GetComponent<EnemyController>();
		_spawnRange = Camera.main.orthographicSize * _spawnRangeMult;
	}

	private void Start()
	{
		GameObject.Find(_bossPoolName).TryGetComponent(out _bossPool);
	}

	public void Init(ElementEnum element, Color color, ObjectPool pool)
	{
		_element = element;
		_color = color;
		_pool = pool;

		_curentTime = 0;

		_enemyController.Init(_element, _color);
	}

	private void Update()
	{
		//CheckSpawnEnemy();
		TrySpawnBoss();
	}

	private void CheckSpawnEnemy()
	{
		_curentTime += Time.deltaTime;
		if (_curentTime > _spawnTime)
		{
			SpawnEnemy();
			_curentTime = 0;
		}
	}

	private void TrySpawnBoss()
	{
		if (_bossPool.GetActiveObjects() == 0)
		{
			Vector3 distVector = Random.insideUnitCircle.normalized * _spawnRange;
			Vector3 pos = _player.transform.position + distVector;

			GameObject gameObject = _bossPool.GetPooledObject();
			gameObject.SetActive(true);
			gameObject.transform.position = pos;
			gameObject.TryGetComponent(out GolemController golemController);
			golemController.Init(GolemState.C, 1);
			golemController.Init(_element, _color);
		}
	}

	private void SpawnEnemy()
	{
		Vector3 distVector = Random.insideUnitCircle.normalized * _spawnRange;
		Vector3 pos = _player.transform.position + distVector;

		GameObject gameObject = _pool.GetPooledObject();
		gameObject.SetActive(true);
		gameObject.transform.position = pos;
		gameObject.TryGetComponent(out EnemyController enemyController);
		enemyController.Init(_element, _color);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = _gizmosColor;
		float _spawnRange = Camera.main.orthographicSize * _spawnRangeMult;
		Gizmos.DrawWireSphere(_player.transform.position, _spawnRange);
	}
}