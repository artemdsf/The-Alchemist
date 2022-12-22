using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject _enemy;
    private GameObject _player;
	private ObjectPool _pool;
	private float _spawnTime;
	private float _spawnRangeMult;
	private float _spawnRange;
	private float _curentTime;

	public void Init(GameObject player, ObjectPool pool, float spawnRangeMult, float spawnTime)
	{
		_player = player;
		_spawnRangeMult = spawnRangeMult;
		_spawnTime = spawnTime;
		_pool = pool;
	}

	private void Start()
	{
		_curentTime = 0;
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
			SpawnEnemy(_enemy);
			_curentTime = 0;
		}
	}

	private void SpawnEnemy(GameObject enemy)
	{
		Vector3 pos = _player.transform.position + 
			(Vector3)Random.insideUnitCircle.normalized * _spawnRange;
		GameObject gameObject = _pool.GetPooledObject();
		gameObject.SetActive(true);
		gameObject.transform.position = pos;
	}
}