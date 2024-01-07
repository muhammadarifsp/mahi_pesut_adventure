using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeName : MonoBehaviour
{
    public TMP_Text playerName;
    public TMP_Text message;
    public TMP_InputField inputFieldName;
    public GameObject btnSubmit;
    public GameObject btnConfirm;
    public GameObject popUpCN;

    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            string dbName = PlayerPrefs.GetString("PlayerName");
            playerName.SetText("Hi, " + dbName);
        }
        else
        {
            playerName.SetText("Hi, Player");
        }
    }

    public void Confirm()
    {
        msg("*submit again to confirm", 1);
        btnSubmit.SetActive(false);
        btnConfirm.SetActive(true);
    }

    public void SubmitName()
    {
        PlayerPrefs.SetString("PlayerName", inputFieldName.text.ToUpper());
        Debug.Log("Name Save, " + PlayerPrefs.GetString("PlayerName"));

        playerName.SetText("Hi, " + PlayerPrefs.GetString("PlayerName"));

        inputFieldName.text = "";
        resetBTN();
        msg("*changed name successfully", 2);
        Close();
    }

    public void Show()
    {
        popUpCN.SetActive(true);
        resetBTN();
    }

    public void Close()
    {
        popUpCN.SetActive(false);
    }

    private void resetBTN()
    {
        btnSubmit.SetActive(true);
        btnConfirm.SetActive(false);
        msg("");
    }

    private void msg(string text, int type = 1)
    {
        message.text = text;
        if (type == 1)
        {
            message.color = new Color32(215, 25, 0, 255);
        }
        else
        {
            message.color = new Color32(15, 150, 0, 255);
        }
    }
}
