using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private GameObject _escapeMenu;
	[SerializeField] private GameObject _UI;

	public static PauseManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		HotKeys.Init();
		SetPause(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(HotKeys.Escape))
		{
			GameManager.IsGamePaused = !GameManager.IsGamePaused;
			SetPause(GameManager.IsGamePaused);
		}
	}

	private void SetPause(bool isPaused)
	{
		if (isPaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
		_escapeMenu?.SetActive(isPaused);
		_UI?.SetActive(!isPaused);
	}
}