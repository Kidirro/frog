using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilySpawner : MonoBehaviour
{
    [SerializeField] private float _poolLimit;
    [SerializeField] private GameObject _lilyPrefab;
    private List<GameObject> _lilyPoll = new List<GameObject>();
    private int _lastLily = 0;
    private int _lastWay = -1;
    [SerializeField] private float _lilySpawnCooldown;
    private List<Vector2> _wayList = new List<Vector2>();

    private Vector2 _lilySize;

    private Vector2 _maxBorder;

    public void CreateSingleLily(Vector3 position)
    {
        if (_lilyPoll.Count==0) StartGame();
        _lastLily = (_lastLily == _lilyPoll.Count - 1) ? 0 : _lastLily + 1;
        _lilyPoll[_lastLily].transform.position = position;
        _lilyPoll[_lastLily].SetActive(true);
    }

    public void StartGame()
    {
        if (_maxBorder ==null)_maxBorder = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        InitializationListLily();
        GravityController.ChangeGloabalGravity(GravityController.GravityStart);
        if(_wayList.Count==0) InitializationListWays();
        StopAllCoroutines();
        StartCoroutine(SpawnLilyProcess());
    }  

    
    
    private void InitializationListLily()
    {
        if (_lilyPoll.Count < _poolLimit)
        {
            for (int i = 0; i < _poolLimit; i++)
            {
                GameObject lilyObj = Instantiate(_lilyPrefab);
                _lilyPoll.Add(lilyObj);
                lilyObj.transform.SetParent(transform);
                lilyObj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject lily in _lilyPoll)
            {
                lily.SetActive(false);
            }
        }
    }

    private void InitializationListWays()
    {
        _wayList = new List<Vector2>();
        Vector2 _minBorder = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 _maxBorder = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        _lilySize = _lilyPrefab.GetComponent<CapsuleCollider2D>().size;
        float lilyWidth = _lilySize.x;

        float widthWorld = _maxBorder.x - _minBorder.x;
        float lilyCount = (int)(widthWorld / lilyWidth / 1.05);
        float lilyDistance = (widthWorld - lilyCount * lilyWidth) / (lilyCount + 1);
        for (int i = 0; i < lilyCount; i++)
        {
            _wayList.Add(new Vector2(_minBorder.x+(lilyDistance+lilyWidth)*(i+1) - lilyWidth / 2, _maxBorder.y + _lilySize.y));

        }
    }

    private IEnumerator SpawnLilyProcess()
    {
        while (true)
        {
            _lastLily = (_lastLily == _lilyPoll.Count-1) ? 0 : _lastLily + 1;
            int wayID = Random.Range(0, _wayList.Count);
            while (wayID == _lastWay)
            {
                wayID = Random.Range(0, _wayList.Count);
            }
            _lastWay = wayID;

            _lilyPoll[_lastLily].transform.position = _wayList[wayID];
            _lilyPoll[_lastLily].SetActive(true);
            GravityController.ChangeGloabalGravity(GravityController.GravityCurrent - GravityController.GravityChangingMultiplier * Time.deltaTime);
            yield return new WaitForSecondsRealtime(_lilySpawnCooldown);
        }
    }
    
}
