using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour 
{
	// Загрузка указанного уровня
	public void LoadScene(string SceneName)
	{
		Application.LoadLevel(SceneName);
	}

	// Загрузка мастерской с указанием планеты
	public void LoadWorkshop(string SceneName)
	{
		PlayerScript.SceneName = SceneName;
		Application.LoadLevel("WorkshopScene");
	}

	// Загрузка указанного уровня с его сохранением в переменной
	public void LoadSceneSaveSceneName(string SceneName)
	{
		PlayerScript.SceneName = SceneName;
		Application.LoadLevel(SceneName);
	}

	// Загрузка уровня из сохраненной ранее переменной
	public void LoadSceneBySceneName()
	{
		Application.LoadLevel(PlayerScript.SceneName);
	}

	// Выход из игры
	public void ExitGame()
	{
		Application.Quit();
	}

}