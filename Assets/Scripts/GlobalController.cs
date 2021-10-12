using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    [SerializeField] private GameObject _border;
    [SerializeField] private static GameObject _borderStat;
    [SerializeField] private LilySpawner _lilySpawner;
    [SerializeField] private static LilySpawner _lilySpawnerStat;
    [SerializeField] private FrogController _frog;
    [SerializeField] private static FrogController _frogStat;
    static public int Score
    {
        get{ return _score;}
    }

    static private int _score;

    private void Start()
    {
        _borderStat = _border;
        _lilySpawnerStat = _lilySpawner;
        _frogStat = _frog;
    }
    public static void StartGame(bool fullReset)
    {
        if (fullReset)
        {
            ResetScore();

            _lilySpawnerStat.StartGame();
            if (_borderStat.transform.position == Vector3.zero)
            {
                _borderStat.transform.position = new Vector2(0, Camera.main.ViewportToWorldPoint(Vector2.zero).y - 5);
                _borderStat.transform.localScale = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, 0)).x * 2, 1);
            }
        }
        _lilySpawnerStat.CreateSingleLily(Vector3.zero);
        _frogStat.FrogReset(Vector3.zero,true);
    }

    static public void AddScore()
    {
        _score++;
        UIScript.AddScore(_score);
    }

    static public void ResetScore()
    {
        _score = 0;
    }

    static public void HideAll()
    {

        _lilySpawnerStat.StartGame();
        _frogStat.FrogReset(Vector3.zero,false);
    }
}
