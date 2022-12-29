using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
	[SerializeField] private ParticleSystem[] particleSystems;
	private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		TryGetComponent(out _spriteRenderer);
	}

	public void ChangeColor(Color color)
	{
		if (_spriteRenderer != null)
			_spriteRenderer.color = color;

		ParticleSystem.MainModule main;
		for (int i = 0; i < particleSystems.Length; i++)
		{
			main = particleSystems[i].main;
			main.startColor = new ParticleSystem.MinMaxGradient(color);
		}
	}
}