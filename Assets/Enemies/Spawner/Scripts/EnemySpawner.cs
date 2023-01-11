using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(EnemyController))]
public class EnemySpawner : MonoBehaviour
{
	private EnemyController _enemyController;
	private GameObject _player;
	private ObjectPool _pool;
	private ElementEnum _element;
	private Color _color;

	private float _spawnRangeMult;
	private float _spawnRange;
	private float _spawnTime;
	private float _curentTime;

	private void Awake()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_enemyController = GetComponent<EnemyController>();
	}

	public void Init(ElementEnum element, Color color, ObjectPool pool, float spawnRangeMult, float spawnTime)
	{
		_element = element;
		_spawnRangeMult = spawnRangeMult;
		_spawnTime = spawnTime;
		_pool = pool;
		_color = color;

		_curentTime = 0;

		_enemyController.Init(_element, _color);

		_spawnRange = Camera.main.orthographicSize * _spawnRangeMult;
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused)
		{
			CheckSpawnEnemy();
		}
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
}