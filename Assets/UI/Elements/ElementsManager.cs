using UnityEngine;

public class ElementsManager : MonoBehaviour
{
	[SerializeField] private Element[] _elements;

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
