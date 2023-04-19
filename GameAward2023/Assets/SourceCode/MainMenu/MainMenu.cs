using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConfirm(InputValue input)
    {
        if(GameObject.Find("GameSystem").GetComponent<GameSystem>().CanInput)
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
