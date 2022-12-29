using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
	[SerializeField] private Tilemap _grassTilemap;
	[SerializeField] private TilesScriptableObject _grassTiles;

    [SerializeField] private Vector2Int _fieldSize = new Vector2Int(20, 10);
	[SerializeField] private GameObject _barrier;

	[Header("Gizmos")]
	[SerializeField] private Color _fieldGizmosColor = Color.white;
	[SerializeField] private Color _barrierGizmosColor;
	private GameObject _currentBarrier;

	private void Awake()
	{
		GameManager.FieldSize = _fieldSize;
	}

	private void Start()
	{
		SpawnTiles(_grassTilemap, _grassTiles, GameManager.FieldSize);
		SpawnAllBarriers();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = _fieldGizmosColor;
		Gizmos.DrawCube(Vector3.zero, (Vector3Int)_fieldSize * 2);

		float cameraSize = Camera.main.orthographicSize;
		Vector2 _barrierSize = new Vector2(_fieldSize.x - cameraSize * 2, _fieldSize.y - cameraSize);
		Gizmos.color = _barrierGizmosColor;
		Gizmos.DrawWireCube(Vector3.zero, _barrierSize * 2);

	}

	private void SpawnTiles(Tilemap tilemap, TilesScriptableObject tiles, Vector2Int size)
	{
		for (int i = -size.x; i < size.x; i++)
		{
			for (int j = -size.y; j < size.y; j++)
			{
				Tile tile = tiles.Tiles[Random.Range(0, tiles.Tiles.Count)];
				tilemap.SetTile(new Vector3Int(i, j, 0), tile);
			}
		}
	}

	private Vector3 _size;
	private Vector3 _pos;

	private void SpawnAllBarriers()
	{
		float cameraSize = Camera.main.orthographicSize;

		_size = new Vector3(cameraSize * 2, _fieldSize.y * 2, 1);
		_pos = Vector3.left * (_fieldSize.x - cameraSize);
		SpawnBarrier(_pos, _size);
		SpawnBarrier(-_pos, _size);
		
		_size = new Vector3(_fieldSize.x * 2 - cameraSize * 4, cameraSize, 1);
		_pos = Vector3.up * (_fieldSize.y - cameraSize / 2);
		SpawnBarrier(_pos, _size);
		SpawnBarrier(-_pos, _size);
	}

	private void SpawnBarrier(Vector3 pos, Vector3 size)
	{
		_currentBarrier = Instantiate(_barrier, pos, Quaternion.identity, transform);
		_currentBarrier.transform.localScale = size;
	}
}
