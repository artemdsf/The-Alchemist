using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
	[SerializeField] private GameObject _spawner;
	[SerializeField] private ObjectPool _pool;
	[SerializeField] private Color[] _elementColors;
	[SerializeField] private float _spawnTime = 1;

	[Header("Spawn range mult")]
	[SerializeField] private float _spawnRangeMult = 2.3f;
	[SerializeField] private float _spawnSpawnerRangeMult = 2;

	[Header("Gizmos")]
	[SerializeField] private GameObject _player;
	[SerializeField] private Color _gizmosColor;

	private float _spawnRange;
	private int _stage = 0;
	private int _elementCount;
	private Vector2 _spawnArea;

	private void Awake()
	{
		_elementCount = System.Enum.GetNames(typeof(ElementEnum)).Length;
		Vector2 barrierSize = Vector2.one * Camera.main.orthographicSize * 2;
		_spawnArea = GameManager.FieldSize - barrierSize;
	}

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
			spriteRenderer.color = _elementColors[(int)element];

		spawner.TryGetComponent(out EnemySpawner enemySpawner);
		enemySpawner?.Init(element, _elementColors[(int)element],
			_pool, _spawnRangeMult, _spawnTime);
	}

	private void StartNextStage()
	{
		_stage++;
		for (int i = 0; i < _stage; i++)
		{
			if (i < _elementCount)
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
		Vector3 pos = new Vector2(
			Random.Range(-_spawnArea.x, _spawnArea.x),
			Random.Range(-_spawnArea.y, _spawnArea.y));

		Vector3 distVector = pos - _player.transform.position;
		if (distVector.magnitude < _spawnRange)
		{
			pos += distVector.normalized * _spawnRange * _spawnSpawnerRangeMult;
		}
		return pos;
	}
}