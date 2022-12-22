using System.Collections.Generic;
using UnityEngine;

public class AttackVisual : MonoBehaviour
{
	public float SafeTime => _safeTime;
	public bool SaveParticles => _saveParticles;

	[SerializeField] private float _safeTime = 1;
	[SerializeField] private bool _saveParticles;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<ParticleSystem> _particles = new List<ParticleSystem>();
    [SerializeField] private List<ParticleSystem> _particlesWithPrewarm = new List<ParticleSystem>();
	private float[] _emissions;
	private float[] _emissionsWithPrewarm;

	private void Awake()
	{
		_emissions = new float[_particles.Count];
		_emissionsWithPrewarm = new float[_particlesWithPrewarm.Count];

		for (int i = 0; i < _particles.Count; i++)
		{
			_emissions[i] = _particles[i].emission.rateOverTime.constant;
		}
		for (int i = 0; i < _particlesWithPrewarm.Count; i++)
		{
			_emissionsWithPrewarm[i] = _particlesWithPrewarm[i].emission.rateOverTime.constant;
		}
		if (_spriteRenderer != null)
		{
			_spriteRenderer.enabled = true;
		}
	}

	public void Init()
	{
		ParticleSystem.EmissionModule emission;
		ParticleSystem.MainModule main;
		for (int i = 0; i < _particles.Count; i++)
		{
			_particles[i].Play();
			emission = _particles[i].emission;
			emission.rateOverTime = _emissions[i];
		}
		for (int i = 0; i < _particlesWithPrewarm.Count; i++)
		{
			_particlesWithPrewarm[i].Play();
			main = _particlesWithPrewarm[i].main;
			main.prewarm = true;
			emission = _particlesWithPrewarm[i].emission;
			emission.rateOverTime = _emissionsWithPrewarm[i];
		}
		if (_spriteRenderer != null)
		{
			_spriteRenderer.enabled = true;
		}
	}

	public void Death()
	{
		if (_particles.Count > 0)
		{
			ParticleSystem.EmissionModule emission;
			foreach (var item in _particles)
			{
				emission = item.emission;
				emission.rateOverTime = 0;
			}
		}

		if (_spriteRenderer != null)
		{
			_spriteRenderer.enabled = false;
		}
	}
}
