using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelledTree : myTree
{
    public override float Collide()
    {
        base.Collide();
        GameManager.instance.AddScore(1);
        return base.Collide();
    }
}
