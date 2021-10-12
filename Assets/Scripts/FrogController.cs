using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : LilyMono
{
    private List<GameObject> _inLilyList;

    [Space]
    [SerializeField] private Sprite _defaultSkinGreen;
    [SerializeField] private Sprite _jumpSkinGreen;
    [SerializeField] private Sprite _deathSkinGreen;

    [Space]
    [SerializeField] private Sprite _defaultSkinRed;
    [SerializeField] private Sprite _jumpSkinRed;
    [SerializeField] private Sprite _deathSkinRed;

    [Space]
    [SerializeField] private Sprite _defaultSkinYellow;
    [SerializeField] private Sprite _jumpSkinYellow;
    [SerializeField] private Sprite _deathSkinYellow;

    [Space]
    private Sprite _defaultSkin;
    private Sprite _jumpSkin; 
    private Sprite _deathSkin;

    [SerializeField] private ParticleSystem _ps;

    [Tooltip("Расстояние до кувшинки")]
    [SerializeField] private float _range;
    private SpriteRenderer _spriteRender;

    public frogState State
    {
        get { return _state; }
    }
    private frogState _state=frogState.dead;


    public void ChangeSkinSet(int id)
    {
        switch (id)
        {
            case 0:
                _defaultSkin = _defaultSkinGreen;
                _jumpSkin = _jumpSkinGreen;
                _deathSkin = _deathSkinGreen;
                break;
            case 1:
                _defaultSkin = _defaultSkinYellow;
                _jumpSkin = _jumpSkinYellow;
                _deathSkin = _deathSkinYellow;
                break;
            case 2:
                _defaultSkin = _defaultSkinRed;
                _jumpSkin = _jumpSkinRed;
                _deathSkin = _deathSkinRed;
                break;

        }
    }

    [Min(0)]
    [SerializeField] private float _jumpingSpeed;

    private void Init()
    {
        _state = frogState.alive;
        _inLilyList = new List<GameObject>();
        _spriteRender = GetComponent<SpriteRenderer>();
        ChangeSkin(_state);
    }

    public IEnumerator Jump(Transform _target)
    {
        SoundController.PlayJumpSound();
        _state = frogState.jump;
        ChangeSkin(_state);
        while (transform.position != _target.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _jumpingSpeed * Time.deltaTime);
            yield return null;
        }

        _state = frogState.alive;
        ChangeSkin(_state);
        if (DeathCheck())  GlobalController.AddScore();
        yield return null;
    }


    public void RotateFrog(Transform target)
    {
        transform.up = target.position - transform.position;
    }

    private void ChangeSkin(frogState stat)
    {
        if (_spriteRender == null) Init ();
        if (_defaultSkin == null) ChangeSkinSet(0);
        switch (stat) {
            case frogState.alive:
                _spriteRender.sprite = _defaultSkin;
                break;
            case frogState.dead:
                _spriteRender.sprite = _deathSkin;
                break;
            case frogState.jump:
                _spriteRender.sprite = _jumpSkin;
                break;
        }
    }

    private bool DeathCheck()
    {
        if (_state == frogState.jump || _inLilyList.Count != 0) return true;
        else {
            _ps.Play();
            SoundController.PlayDeathSound();
            UIScript.ShowMenu(true);
            _state = frogState.dead;
            ChangeSkin(_state);           
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        _inLilyList.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _inLilyList.Remove(collision.gameObject);
        DeathCheck();
    }

    public void FrogReset(Vector3 pos,bool alive)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (alive)_state = frogState.alive;
        ChangeSkin(_state);
        transform.position = pos;
    }
}

public enum frogState
{
    alive,
    jump,
    dead
}