using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public TMP_InputField characterName;
    public static string BestnameInGame;
    public static string nameInGame;
    public static int BestScore;


    public void StartGame()
    {
        LoadBestScore();
        nameInGame = characterName.text;
        SceneManager.LoadScene(1);
    }
    [System.Serializable]
    public class MenuUISer
    {
        public string Bestname;
        public int bestScore;
    }

    public void SaveBestScore()
    {
        MenuUISer data = new MenuUISer();
        data.Bestname = BestnameInGame;
        data.bestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MenuUISer data = JsonUtility.FromJson<MenuUISer>(json);

            BestnameInGame = data.Bestname;
            BestScore = data.bestScore;
        }
    }

    public void Exit()
    {
        SaveBestScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
