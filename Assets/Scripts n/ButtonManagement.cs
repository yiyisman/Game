using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManagement : MonoBehaviour {


    public Canvas his;
    public Canvas exitMenu;
    public Button exitYes;
    public Button exitNO;
    public Canvas instruc;

    private void Start()
    {
    }
    // Use this for initialization
    public void NewGameBtn (string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
	}
    public void His()
    {
        his.enabled = true;
        exitMenu.enabled = false;
    }
    public void ins()
    {
        instruc.enabled = true;
        exitMenu.enabled = false;
    }
    public void nins()
    {
        instruc.enabled = false;
        exitMenu.enabled = true;
    }

    // Update is called once per frame
    public void ExitGameBtn()
    {

        Application.Quit();
        Debug.Log("salio");
    }

}
