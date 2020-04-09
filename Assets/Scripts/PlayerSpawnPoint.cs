using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("register");
        this.RegisterSelf();
    }

    private void RegisterSelf()
    {
        GameManager.Instance.RegisterSpawnPoint(this);
    }
}
