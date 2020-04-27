using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellLoad : MyScene
{
    [SerializeField]
    private Hacker hacker;
    [SerializeField]
    private CellGuardMovement[] guards;
    [SerializeField]
    private DoorController cellDoor;

    public override void OnUnload()
    {
        base.OnUnload();
        CellConstants.buttonsUnlocked = false;
        this.hacker.ClosePanel();
        this.hacker.alreadyCrackPassword = false;
        foreach (CellGuardMovement guard in this.guards) guard.Reset();
        this.cellDoor.CloseDoorAction();
    }
}
