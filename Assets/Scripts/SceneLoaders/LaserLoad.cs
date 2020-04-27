using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLoad : MyScene
{
    public override void OnLoad()
    {
        base.OnLoad();
        GameManager.Instance.SetPlayerHeadSmall(false);
    }

    public override void OnUnload()
    {
        base.OnUnload();
    }
}
