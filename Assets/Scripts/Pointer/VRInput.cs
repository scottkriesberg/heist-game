using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInput : BaseInput
{
    public Camera eventCamera = null;
    public SteamVR_Action_Boolean m_ClickAction;
    public SteamVR_Input_Sources m_InputSource;

    protected override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;
    }

    public override bool GetMouseButton(int button)
    {
        return m_ClickAction.GetStateDown(m_InputSource);
    }

    public override bool GetMouseButtonDown(int button)
    {
        return true;
    }

    public override bool GetMouseButtonUp(int button)
    {
        return true;
    }

    public override Vector2 mousePosition
    {
        get
        {
            return Vector2.zero;
        }
    }
}
