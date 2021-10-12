using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChecker : MonoBehaviour
{

    private FrogController _frog;

    [SerializeField] private float _multiplierPower;

    private Vector2 _touchedPosition;
    private Vector2 _pullVector;

    [SerializeField] private GameObject _dots;

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _anchor;

    private Vector2 _minBorder;
    private Vector2 _maxBorder;

    IEnumerator _dragProcess;

    private void Awake()
    {
        _minBorder = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        _maxBorder = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight)); 
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.size =new Vector2(_maxBorder.x-_minBorder.x,_maxBorder.y-_minBorder.y);
        _frog = FindObjectOfType<FrogController>();
        _dots.gameObject.SetActive(false);
        _dragProcess = DragProcces();
    }

    private Vector2 CalculateTargetPoint(float x, float y)
    {
        return new Vector2(Mathf.Clamp(x, _minBorder.x, _maxBorder.x), Mathf.Clamp(y, _minBorder.y, _maxBorder.y));
    }


    public void OnFrogDown()
    {
        if (_frog.State ==frogState.alive)
        {
            _dots.gameObject.SetActive(true);
            _touchedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _target.SetActive(true);
            _anchor.SetActive(true);
            _anchor.transform.position = _touchedPosition;
            StartCoroutine(_dragProcess);
        }
    }

    public void OnFrogUp()
    {
        if (_target.activeSelf )
        {

            _dots.gameObject.SetActive(false);
            _target.SetActive(false);
            _anchor.SetActive(false);
            StopCoroutine(_dragProcess);
            if ( (_target.transform.position - _frog.transform.position).magnitude > 1)
                StartCoroutine(_frog.Jump(_target.transform));

        }
    }

    private IEnumerator DragProcces()
    {
        while (true)
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _pullVector = new Vector2(_touchedPosition.x - currentPosition.x, _touchedPosition.y - currentPosition.y);
            _target.transform.position = CalculateTargetPoint(_frog.transform.position.x + _pullVector.x * _multiplierPower, _frog.transform.position.y + _pullVector.y * _multiplierPower);
            _frog.RotateFrog(_target.transform);
            LineScript.ShowTrajectory(_dots.transform.position, _target.transform.position, 0);

            yield return null;
        }
    }
}
