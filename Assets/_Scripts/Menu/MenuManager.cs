using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int active_menu = 0;
    public GameObject[] text_menu;

    private void Update()
    {
        for (int i = 0; i < text_menu.Length; i++)
        {
            text_menu[i].SetActive(false);

            if (i == active_menu)
            {
                text_menu[i].SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
