using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(EnemyHealth))]
public class EnemySpawner : MonoBehaviour
{
	private EnemyHealth _enemyHealth;
	private GameObject _player;
	private ObjectPool _pool;
	private ElementEnum _element;
	private Color _color;
	private float _spawnTime;
	private float _spawnRangeMult;
	private float _spawnRange;
	private float _curentTime;

	public void Init(ElementEnum element, Color color, GameObject player, 
		ObjectPool pool, float spawnRangeMult, float spawnTime)
	{
		_curentTime = 0;
		_element = element;
		_player = player;
		_spawnRangeMult = spawnRangeMult;
		_spawnTime = spawnTime;
		_pool = pool;
		_color = color;

		_enemyHealth = GetComponent<EnemyHealth>();
		_enemyHealth.Init(_element, _color);

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
		Vector3 pos = _player.transform.position + 
			(Vector3)Random.insideUnitCircle.normalized * _spawnRange;
		GameObject gameObject = _pool.GetPooledObject();
		gameObject.SetActive(true);
		gameObject.transform.position = pos;
		gameObject.TryGetComponent(out EnemyHealth enemyHealth);
		enemyHealth.Init(_element, _color);
	}
}