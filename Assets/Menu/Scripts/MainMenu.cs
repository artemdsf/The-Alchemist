using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject _mainMenu;
	[SerializeField] private GameObject _settingMenu;

	private void Start()
	{
		_settingMenu.SetActive(false);
	}

	public void StartButton(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName);
	}

	public void SettingButton()
	{
		_settingMenu.SetActive(true);
		_mainMenu.SetActive(false);
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
