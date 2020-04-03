using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.RegisterSpawnPoint(this);
    }
}
