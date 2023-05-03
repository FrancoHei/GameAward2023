using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoopSelect : MonoBehaviour
{
	//　最初にフォーカスするゲームオブジェクト
	[SerializeField]
	private GameObject firstSelect;
	[SerializeField]
	private GameObject statusWindow;
	public void ActivateOrNotActivate(bool flag)
	{
		for (var i = 0; i < transform.childCount; i++)
		{
			Button button = transform.GetChild(i).GetComponent<Button>();
			if (button)
				button.interactable = flag;
		}
		if (flag)
		{
			EventSystem.current.SetSelectedGameObject(firstSelect);
		}
	}

	public void OnContinue()
	{
		Time.timeScale = 1;
		statusWindow.SetActive(false);
	}

	public void BackStageSelect()
	{

	}

	public void BackTitle()
	{
		SceneManager.LoadScene("Title", LoadSceneMode.Single);
	}

}
