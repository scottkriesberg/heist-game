using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardLoad : MyScene
{
    [SerializeField]
    private GuardMovement[] guards;

    public override void OnUnload()
    {
        base.OnUnload();
        foreach (GuardMovement guard in this.guards) guard.Reset();
    }
}
