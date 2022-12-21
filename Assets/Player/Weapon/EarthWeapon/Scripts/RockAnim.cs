using UnityEngine;

public class RockAnim : MonoBehaviour
{
	[SerializeField] private Gradient _gradient;
	[SerializeField] private float _gradSpeed;
    private Material _material; 
	private float _color;

	private void Awake()
	{
		_material = GetComponent<Renderer>().material;
	}

	private void Update()
	{
		_color += Time.deltaTime * _gradSpeed;
		_color %= 1;	
		_material.SetVector("_EmissionColor", _gradient.Evaluate(_color));
	}
}
