using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
	[SerializeField] private GameObject _spawner;
	[SerializeField] private Color[] _colors;
	[SerializeField] private float _spawnTime = 1;
	[SerializeField] private ObjectPool _pool;
	[Header("Spawn range mult")]
	[SerializeField] private float _spawnRangeMult = 2.3f;
	[SerializeField] private float _spawnSpawnerRangeMult = 2;
	[Header("Gizmos")]
	[SerializeField] private Color _gizmosColor;
	[SerializeField] private GameObject _player;
	private float _spawnRange;
	private int _stage = 0;

	private void Start()
	{
		_spawnRange = Camera.main.orthographicSize * _spawnRangeMult;
	}

	private void Update()
	{
		if (IsStageEnded() && _stage >= 0)
		{
			StartNextStage();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = _gizmosColor;
		float _spawnRange = Camera.main.orthographicSize * _spawnRangeMult;
		Gizmos.DrawWireSphere(_player.transform.position, _spawnRange);
	}

	private void SetSpawner(ElementEnum element, Vector2 pos)
	{
		GameObject spawner = Instantiate(_spawner, pos, Quaternion.identity, transform);
		spawner.TryGetComponent(out SpriteRenderer spriteRenderer);
		if (spriteRenderer != null)
			spriteRenderer.color = _colors[(int)element];
		spawner.TryGetComponent(out EnemySpawner enemySpawner);
		enemySpawner?.Init(element, _colors[(int)element], _player,
			_pool, _spawnRangeMult, _spawnTime);
	}

	private void StartNextStage()
	{
		_stage++;
		for (int i = 0; i < _stage; i++)
		{
			if (i < System.Enum.GetNames(typeof(ElementEnum)).Length)
			{
				SetSpawner((ElementEnum)i, GetSpawnPosition());
			}
			else
			{
				StartFinalStage();
			}
		}
	}

	private void StartFinalStage()
	{
		Debug.Log("Final stage");
		_stage = -1;
	}

	private bool IsStageEnded()
	{
		if (transform.childCount > 0)
			return false;
		return true;
	}

	private Vector3 GetSpawnPosition()
	{
		Vector2 spawnArea = GameManager.FieldSize - Vector2.one * Camera.main.orthographicSize * 2;
		Vector3 pos = new Vector2(
			Random.Range(-spawnArea.x, spawnArea.x),
			Random.Range(-spawnArea.y, spawnArea.y));
		if ((pos - _player.transform.position).magnitude < _spawnRange)
		{
			pos += (pos - _player.transform.position).normalized * 
				_spawnRange * _spawnSpawnerRangeMult;
		}
		return pos;
	}
}