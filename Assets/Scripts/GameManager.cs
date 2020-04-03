using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform playerPrefab;

    public Transform currPlayer;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void RegisterSpawnPoint(PlayerSpawnPoint point)
    {
        if (this.currPlayer == null)
        {
            this.currPlayer = GameObject.Instantiate(this.playerPrefab);
            DontDestroyOnLoad(this.currPlayer.gameObject);
        }

        this.currPlayer.position = point.transform.position;
        this.currPlayer.rotation = point.transform.rotation;
    }
}
