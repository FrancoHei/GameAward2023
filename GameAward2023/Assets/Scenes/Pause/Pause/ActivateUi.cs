using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ActivateUi : MonoBehaviour
{
	//�@���UI
	[SerializeField]
	private GameObject statusWindow;
	//�@�{�^���̃C���^���N�e�B�u�Ɋւ��鏈����������Ă���X�N���v�g
	[SerializeField]
	private LoopSelect select1;
	[SerializeField]
	private LoopSelect select2;

	void Update()
	{

		//�@q������������UI�̃I���E�I�t
		if (Input.GetKeyDown("q") || Input.GetKeyDown("joystick button 6"))
		{
			statusWindow.SetActive(!statusWindow.activeSelf);

			//�@��ʂ��J��������Background1�̃{�^���̃C���^���N�e�B�u��true�ABackground2�̃{�^���̃C���^���N�e�B�u��false�ɂ���
			if (statusWindow.activeSelf)
			{
				PauseGame();
                select1.ActivateOrNotActivate(true);
				select2.ActivateOrNotActivate(false);

                //�@��ʂ������I��������
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
