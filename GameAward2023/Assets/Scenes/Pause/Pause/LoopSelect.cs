using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LoopSelect : MonoBehaviour
{
	//　最初にフォーカスするゲームオブジェクト
	[SerializeField]
	private GameObject firstSelect;
	public  GameObject statusWindow;
	public void ActivateOrNotActivate(bool flag)
	{
		for (var i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild(i).GetComponent<Button>()) 
				transform.GetChild(i).GetComponent<Button>().interactable = flag;
		}
		if (flag)
		{
			EventSystem.current.SetSelectedGameObject(firstSelect);
		}
	}

	public void GoStageSelect() 
	{
		SceneManager.LoadScene("FKD_StageSelect", LoadSceneMode.Single);
	}

	public void Continue() 
	{
		statusWindow.SetActive(!statusWindow.activeSelf);
		DepthOfField dof;
		GameObject.Find("Volumn").GetComponent<Volume>().profile.TryGet(out dof);
		dof.active = false;
		GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput = true;
	}

	public void GoTitle() 
	{
		SceneManager.LoadScene("Title", LoadSceneMode.Single);
	}
}
