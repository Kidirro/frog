using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GravityController
{
    static private List<LilyMono> _objects = new List<LilyMono>();
    
    static public float GravityStart
    {
        get
        {
            return _gravityStart;
        }
    }
    static private float _gravityStart = -2;


    static public float GravityChangingMultiplier
    {
        get
        {
            return _gravityChangingMultiplier;
        }
    }
   static private float _gravityChangingMultiplier = 20;


    static public float GravityCurrent
    {
        get { 
            return _gravityCurrent;
        }
    }
    static private float _gravityCurrent=0;

    static public void AddLily(LilyMono obj)
    {
        _objects.Add(obj);
        obj.ChangeVelocity(_gravityCurrent);
    }

    static public void ChangeGloabalGravity(float newGravity)
    {
        foreach(LilyMono lily in _objects)
        {
            lily.ChangeVelocity(newGravity);
        }
        _gravityCurrent = newGravity;
    }

    static public void RemoveLily(LilyMono obj)
    {
        _objects.Remove(obj);
    }
}
