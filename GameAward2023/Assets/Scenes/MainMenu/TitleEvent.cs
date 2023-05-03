using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleEvent : MonoBehaviour
{
    [SerializeField,Tooltip("���[�h����V�[����")] private string SceneName; 
    
    public void TriggerInput()
    {
        SceneManager.LoadScene(SceneName);

    }
}
