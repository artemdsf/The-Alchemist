using UnityEngine;

public class ElementsManagerUI : MonoBehaviour
{
	[SerializeField] private ElementIcon[] _elements;

	public void SelectElement(int element)
	{
		foreach (var item in _elements)
		{
			item.UnSelect();
		}

		_elements[element].Select();
	}

	public void SetReloadBar(int element, float amount)
	{
		_elements[element].SetReloadBar(amount);
	}
}
