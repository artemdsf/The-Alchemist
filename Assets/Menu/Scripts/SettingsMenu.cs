using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] private GameObject _mainMenu;
	[SerializeField] private GameObject _settingsMenu;

	public void BackButton()
	{
		_mainMenu.SetActive(true);
		_settingsMenu.SetActive(false);
	}
}
