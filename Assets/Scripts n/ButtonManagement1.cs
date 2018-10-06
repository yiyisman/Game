using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManagement1 : MonoBehaviour {


    public Canvas exitMenu;
    public Button exitYes;
    public Button exitNO;

    private void Start()
    {
    }
    // Use this for initialization
    public void NewGameBtn (string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
	}
	
	// Update is called once per frame
	public void ExitGameBtn()
    {

        Application.Quit();
        Debug.Log("salio");
    }

}
