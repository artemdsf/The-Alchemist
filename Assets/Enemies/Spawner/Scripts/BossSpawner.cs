using UnityEngine;

public class BossSpawner : EnemySpawner
{
	[SerializeField] private string _bossPoolName;

	private ObjectPool _bossPool;

	private void Start()
	{
		GameObject.Find(_bossPoolName).TryGetComponent(out _bossPool);
	}

	protected override void Update()
	{
		TrySpawnBoss();
	}

	private void TrySpawnBoss()
	{
		if (_bossPool.GetActiveObjects() == 0)
		{
			Vector3 distVector = Random.insideUnitCircle.normalized * spawnRange;
			Vector3 pos = player.transform.position + distVector;

			GameObject gameObject = _bossPool.GetPooledObject();
			gameObject.SetActive(true);
			gameObject.transform.position = pos;
			gameObject.TryGetComponent(out GolemController golemController);
			golemController.InitBoss(element, color, 1);
		}
	}
}