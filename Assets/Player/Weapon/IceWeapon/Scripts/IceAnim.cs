using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAnim : MonoBehaviour
{
	[SerializeField] Gradient _gradient;
	private SpriteRenderer _spriteRenderer;
	private float _time;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused)
		{
			_time += Time.deltaTime;
			_spriteRenderer.color = _gradient.Evaluate(_time % 1);
		}
	}
}
