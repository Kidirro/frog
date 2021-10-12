using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Text _score;

    [SerializeField] private Text _scoreMenu;
    [SerializeField] private Text _recordMenu;

    [SerializeField] private Text _globalRecordMenu;

    [SerializeField] private GameObject _mainMenu;

    private static Text _scoreStat;
    private static Text _scoreMenuStat;
    private static Text _recordMenuStat;
    private static GameObject  _menuStat;

    private void Awake()
    {
        _globalRecordMenu.text ="RECORD\n" + PlayerPrefs.GetString("Record");
        _scoreMenuStat = _scoreMenu;
        _recordMenuStat = _recordMenu;
        _scoreStat = _score;
        _menuStat = _menu;
        _scoreStat.text = "0";
    }

    public void RestartGame(bool ResetMode)
    {
       
        ShowMenu(false);
        _scoreStat.gameObject.SetActive(true);
        if (!ResetMode)
        {
            AdsController.ShowRewardedVideo();
        }
        else
        {
            AddScore(0);
            _mainMenu.SetActive(false);
        }
        GlobalController.StartGame(ResetMode);
    }

    public static void AddScore(int value)
    {
        _scoreStat.text = value.ToString();
    }


    public static void ShowMenu (bool state)
    {
        _menuStat.SetActive(state);
        _scoreMenuStat.text = _scoreStat.text;
        if (PlayerPrefs.GetString("Record").Length > 0)
        {
            if (int.Parse(_scoreStat.text) > int.Parse(PlayerPrefs.GetString("Record")))
            {
                PlayerPrefs.SetString("Record", _scoreStat.text);
            }
        }
        else
        {

            PlayerPrefs.SetString("Record", _scoreStat.text);
        }
        _recordMenuStat.text = PlayerPrefs.GetString("Record");
    }

    public void GoHome()
    {
        GravityController.ChangeGloabalGravity(0);
        ShowMenu(false);
        _mainMenu.SetActive(true);
        _scoreStat.gameObject.SetActive(false);
        GlobalController.HideAll();
    }
}
