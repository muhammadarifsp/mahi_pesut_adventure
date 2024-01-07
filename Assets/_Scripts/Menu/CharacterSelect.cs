using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    private int indexCharacter;
    public GameObject[] character;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < character.Length; i++)
        {
            character[i].SetActive(false);
            if (i == indexCharacter)
            {
                character[i].SetActive(true);
            }
        }
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("CharSelect", indexCharacter);
        Debug.Log(PlayerPrefs.GetInt("CharSelect"));
    }

    public void NextCharacter()
    {
        if (indexCharacter < character.Length - 1)
        {
            indexCharacter++;
        }
        else
        {
            indexCharacter = 0;
        }
    }

    public void PrevCharacter()
    {
        if (indexCharacter > 0)
        {
            indexCharacter--;
        }
        else
        {
            indexCharacter = character.Length - 1;
        }
    }
}
