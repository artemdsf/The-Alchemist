using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAnim : MonoBehaviour
{
	[SerializeField] Gradient _gradient;
	private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		_spriteRenderer.color = _gradient.Evaluate(Time.time % 1);
	}
}
