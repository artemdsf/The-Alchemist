using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
	[SerializeField] private ParticleSystem[] particleSystems;

	public void ChangeColor(Color color)
	{
		TryGetComponent(out SpriteRenderer spriteRenderer);
		if (spriteRenderer != null)
			spriteRenderer.color = color;
		ParticleSystem.MainModule main;
		for (int i = 0; i < particleSystems.Length; i++)
		{
			main = particleSystems[i].main;
			main.startColor = new ParticleSystem.MinMaxGradient(color);
		}
	}
}