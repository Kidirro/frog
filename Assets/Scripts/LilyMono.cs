using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyMono : MonoBehaviour
{

    private Rigidbody2D _rb2D;

    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        GravityController.AddLily(this);
    }

    public void ChangeVelocity(float newGravity)
    {
        _rb2D.velocity = new Vector2(0, newGravity);

    }

    public void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
        ChangeVelocity(GravityController.GravityCurrent);
    }

    private void OnDestroy()
    {
        GravityController.RemoveLily(this);
    }
}
