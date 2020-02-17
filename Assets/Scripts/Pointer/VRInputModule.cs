using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera m_Camera;
    public SteamVR_Input_Sources m_InputSource;
    public SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_CurrentEventData = null;

    protected override void Awake()
    {
        base.Awake();
        m_CurrentEventData = new PointerEventData(eventSystem); // where does the eventSystem come from
    }

    public override void Process()
    {
        // Reset data, set camera
        m_CurrentEventData.Reset();
        m_CurrentEventData.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

        // raycast
        eventSystem.RaycastAll(m_CurrentEventData, m_RaycastResultCache);
        m_CurrentEventData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_CurrentEventData.pointerCurrentRaycast.gameObject;

        // clear rayCast
        m_RaycastResultCache.Clear();

        // Hover
        HandlePointerExitAndEnter(m_CurrentEventData, m_CurrentObject);

        // Press
        if (m_ClickAction.GetStateDown(m_InputSource))
        {
            Debug.Log("Press");
            ProcessPress(m_CurrentEventData);
        }

        // Release
        if (m_ClickAction.GetStateUp(m_InputSource))
        {
            ProcessRelease(m_CurrentEventData);
        }
    }

    public PointerEventData GetData()
    {
        return m_CurrentEventData;
    }

    private void ProcessPress(PointerEventData data)
    {
        // set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // check for object hit, get the downhandler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        // if no downHandler, try and get click handler
        if (newPointerPress == null)
        {
            // newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerClickHandler);
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject); // what is the difference???
        }

        // Set data
        data.pressPosition = data.position; 
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        // executer the pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // Check for a click handler
        GameObject upHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // Check if actual 
        if (upHandler == data.pointerPress)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        // clear the gameobject
        eventSystem.SetSelectedGameObject(null);

        // Reset the data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }

}
