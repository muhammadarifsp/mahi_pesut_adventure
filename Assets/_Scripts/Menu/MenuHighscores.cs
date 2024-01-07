using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuHighscores : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public Transform emptyHighscore;
    public Transform buttonContainer;
    private List<Transform> highscoreEntryTransforms;
    private bool myNameExist;

    private void Awake()
    {
        // matikan container dan template buat entry highscore
        buttonContainer.gameObject.SetActive(false);
        entryTemplate.gameObject.SetActive(false);

        // PlayerPrefs.DeleteKey("Highscores");
        // AddHighscoreEntry("erik", 999);

        HighscoreList highscoreList = new HighscoreList();
        highscoreList.highscoreEntries = new List<HighscoreEntry>();

        // load highscore
        if (PlayerPrefs.HasKey("Highscores"))
        {
            string jsonString = PlayerPrefs.GetString("Highscores");
            highscoreList = JsonUtility.FromJson<HighscoreList>(jsonString);

            //sorting highscore dari yg scorenya terbesar
            for (int i = 0; i < highscoreList.highscoreEntries.Count; i++)
            {
                for (int j = i + 1; j < highscoreList.highscoreEntries.Count; j++)
                {
                    if (
                        highscoreList.highscoreEntries[j].score
                        > highscoreList.highscoreEntries[i].score
                    )
                    {
                        //swap
                        HighscoreEntry temp = highscoreList.highscoreEntries[i];
                        highscoreList.highscoreEntries[i] = highscoreList.highscoreEntries[j];
                        highscoreList.highscoreEntries[j] = temp;
                    }
                }
            }

            highscoreEntryTransforms = new List<Transform>();
            int highscoresLength = highscoreList.highscoreEntries.Count; // panjang savean highscore

            // pengecekan biar tabel highscore ga lebih dari 10
            if (highscoresLength > 10)
            {
                highscoresLength = 10;
            }

            // perulangan buat membuat daftar tiap baris di layar
            for (int i = 0; i < highscoresLength; i++)
            {
                // fungsi nambah entry highscore
                CreateHighscoreEntry(
                    highscoreList.highscoreEntries[i],
                    entryContainer,
                    highscoreEntryTransforms
                );

                // cek nama buat share button
                string name = PlayerPrefs.GetString("PlayerName", "");
                // proses ngecek nama kalo ada di list highscore atau gak
                if (name != "" && myNameExist == false)
                {
                    // cek jika nama ada
                    if (highscoreList.highscoreEntries[i].name == name)
                    {
                        myNameExist = true; // ubah variabel bool jadi true
                    }
                }
            }
        }
        else
        {
            // jika belum ada data highscore
            emptyHighscore.gameObject.SetActive(true);
        }

        // foreach (HighscoreEntry highscoreEntry in highscoreList.highscoreEntries)
        // {
        //     CreateHighscoreEntry(highscoreEntry, entryContainer, highscoreEntryTransforms);
        // }

        // buat button share dipaling bawah tabel
        Transform buttonTransform = Instantiate(buttonContainer, entryContainer);

        // nyalakan button share jika namanya ada di list highscores
        if (myNameExist)
        {
            buttonTransform.gameObject.SetActive(true);
            // buttonTransform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
        }
    }

    // fungsi buat data highscore untuk ditampilkan di tabel highscore
    private void CreateHighscoreEntry(
        HighscoreEntry highscoreEntry,
        Transform container,
        List<Transform> transformList
    )
    {
        // buat objek datanya
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        entryTransform.gameObject.SetActive(true);

        // proses edit rank
        int rank = transformList.Count + 1;
        string rankText;
        // penamaan rank
        switch (rank)
        {
            case 1:
                rankText = "1ST";
                break;
            case 2:
                rankText = "2ND";
                break;
            case 3:
                rankText = "3RD";
                break;
            default:
                rankText = rank + "TH";
                break;
        }
        // masukkan rank
        entryTransform.Find("rankText").GetComponent<TMP_Text>().text = rankText;

        // masukkan nama
        string playerName = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = playerName;

        //masukkan skor
        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = score.ToString();

        // custom style buat rank 1
        if (rank == 1)
        {
            entryTransform.Find("rankText").GetComponent<TMP_Text>().color = Color.magenta;
            entryTransform.Find("rankText").GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
            entryTransform.Find("rankText").GetComponent<TMP_Text>().fontSize = 100;

            entryTransform.Find("nameText").GetComponent<TMP_Text>().color = Color.magenta;
            entryTransform.Find("nameText").GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
            entryTransform.Find("nameText").GetComponent<TMP_Text>().fontSize = 100;

            entryTransform.Find("scoreText").GetComponent<TMP_Text>().color = Color.magenta;
            entryTransform.Find("scoreText").GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
            entryTransform.Find("scoreText").GetComponent<TMP_Text>().fontSize = 100;
        }
        // masukkan entrynya kedalam list transform
        transformList.Add(entryTransform);
    }

    // fungsi untuk kalo mau nambah higshcore (dibuat static biar bisa diakses dari luar script)
    public static void AddHighscoreEntry(string name, int score)
    {
        // buat data highscore yg bakal disimpan
        HighscoreEntry highscoreEntry = new HighscoreEntry { name = name.ToUpper(), score = score };
        // buat variabel penampung load highscores
        HighscoreList highscoreList = new HighscoreList();
        highscoreList.highscoreEntries = new List<HighscoreEntry>();
        // load highscores
        if (PlayerPrefs.HasKey("Highscores"))
        {
            string jsonString = PlayerPrefs.GetString("Highscores");
            highscoreList = JsonUtility.FromJson<HighscoreList>(jsonString);
        }

        //tambah data higshcore baru
        highscoreList.highscoreEntries.Add(highscoreEntry);

        //parse balik ke string json dan simpan
        string json = JsonUtility.ToJson(highscoreList);
        PlayerPrefs.SetString("Highscores", json);
        PlayerPrefs.Save();
    }

    private class HighscoreList
    {
        public List<HighscoreEntry> highscoreEntries;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
