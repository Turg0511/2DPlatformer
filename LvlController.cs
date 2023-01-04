using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{
    public GameObject menu;
    public void Lvl1()
    {
        SceneManager.LoadScene("lvl_1");
    }

    public void Lvl2()
    {
        SceneManager.LoadScene("lvl_2");
    }

    public void Lvl3()
    {
        SceneManager.LoadScene("lvl_3");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Complete()
    {
        SceneManager.LoadScene("Congrats");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Title_Screen");
        menu.SetActive(false);
    }
}
