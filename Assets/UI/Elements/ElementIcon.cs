using UnityEngine;
using UnityEngine.UI;

public class ElementIcon : MonoBehaviour
{
    [SerializeField] private Image _frame;
	[SerializeField] private float _selectionScale = 1.15f;

	public void Select()
	{
		gameObject.transform.localScale = Vector3.one * _selectionScale;
	}

	public void UnSelect()
	{
		gameObject.transform.localScale = Vector3.one;
	}

	public void SetReloadBar(float amount)
	{
		_frame.fillAmount = amount;
	}
}
