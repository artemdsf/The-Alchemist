using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public void LoadButton(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName);
	}
}
