using UnityEngine;

public class EnemyColor : MonoBehaviour
{
	[SerializeField] private ParticleSystem[] _particleSystems;
	private SpriteRenderer _spriteRenderer;

	public void ChangeColor(Color color)
	{
		if (_spriteRenderer != null)
			_spriteRenderer.color = color;

		ParticleSystem.MainModule main;
		for (int i = 0; i < _particleSystems.Length; i++)
		{
			main = _particleSystems[i].main;
			main.startColor = new ParticleSystem.MinMaxGradient(color);
		}
	}

	private void Awake()
	{
		TryGetComponent(out _spriteRenderer);
	}
}