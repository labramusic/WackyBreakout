using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButtonOnClick()
    {
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    public void QuitButtonOnClick()
    {

        Application.Quit();
    }
}
