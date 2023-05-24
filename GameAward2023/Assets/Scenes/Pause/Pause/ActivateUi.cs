using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ActivateUi : MonoBehaviour
{
	//　画面UI
	[SerializeField]
	private GameObject statusWindow;
	//　ボタンのインタラクティブに関する処理が書かれているスクリプト
	[SerializeField]
	private LoopSelect select1;
	[SerializeField]
	private LoopSelect select2;

	void Update()
	{

		//　qを押したら画面UIのオン・オフ
		if (Input.GetKeyDown("q") || Input.GetKeyDown("joystick button 6"))
		{
			statusWindow.SetActive(!statusWindow.activeSelf);

			//　画面を開いた時にBackground1のボタンのインタラクティブをtrue、Background2のボタンのインタラクティブをfalseにする
			if (statusWindow.activeSelf)
			{
				PauseGame();
                select1.ActivateOrNotActivate(true);
				select2.ActivateOrNotActivate(false);

                //　画面を閉じたら選択を解除
            }
            else
			{
				ResumeGame();
				EventSystem.current.SetSelectedGameObject(null);
				
			}
		}
	}
	public void PauseGame()
	{
		DepthOfField dof;
		GameObject.Find("Volumn").GetComponent<Volume>().profile.TryGet(out dof);
		dof.active = true;
		GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput = false;
	}

	public void ResumeGame()
	{
		DepthOfField dof;
		GameObject.Find("Volumn").GetComponent<Volume>().profile.TryGet(out dof);
		dof.active = false;
		GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput = true;
	}

}
