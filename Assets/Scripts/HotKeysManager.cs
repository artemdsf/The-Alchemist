using UnityEngine;

public class HotKeysManager : MonoBehaviour
{
	[SerializeField] private GameObject _escapeMenu;
	[SerializeField] private GameObject _UI;

	public static HotKeysManager Instance;

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
			SetPause(!GameManager.IsGamePaused);
		}
	}

	private void SetPause(bool isPaused)
	{
		GameManager.IsGamePaused = isPaused;
		_escapeMenu?.SetActive(isPaused);
		_UI?.SetActive(!isPaused);
	}
}