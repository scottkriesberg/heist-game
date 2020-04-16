using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    private static float enableSourceDot = 0.35f;
    private static float enablePipesStartDot = 0.45f;
    private static float enablePipesEndDot = 0.55f;
    private static float enableCanvasDot = 0.545f;
    private static float[] enableTextDots = { 0.6f, 0.65f, 0.675f, 0.7f, 0.75f };
    private static float enableStatusDot = 0.8f;

    [SerializeField]
    private Material sourceLightMat;
    [SerializeField]
    private GameObject[] pipes;
    [SerializeField]
    private GameObject mainCanvas;
    [SerializeField]
    private GameObject[] timerTexts;
    [SerializeField]
    private GameObject statusText;
    [SerializeField]
    private Transform watchForwardTransform;

    private Transform playerCamTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.TryGetPlayerCam());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerCamTransform == null || this.watchForwardTransform == null) return;

        float dot = Vector3.Dot(this.watchForwardTransform.forward, this.playerCamTransform.forward);
        this.sourceLightMat.SetColor("_EmissionColor", dot > enableSourceDot ? Color.cyan : Color.black);

        Vector3 pipeScale = new Vector3(1, 1, this.GetPipeScaleFromDot(dot));
        foreach (GameObject pipe in this.pipes)
        {
            pipe.transform.localScale = pipeScale;
        }

        this.mainCanvas.gameObject.SetActive(dot > enableCanvasDot);
        for (int i = 0; i < 5; i++)
        {
            this.timerTexts[i].gameObject.SetActive(dot > enableTextDots[i]);
        }

        this.statusText.SetActive(dot > enableStatusDot);
    }

    private float GetPipeScaleFromDot(float dot)
    {
        if (dot < enablePipesStartDot) return 0;
        if (dot > enablePipesEndDot) return 1;
        return (dot - enablePipesStartDot) / (enablePipesEndDot - enablePipesStartDot);
    }

    private IEnumerator TryGetPlayerCam()
    {
        while (this.playerCamTransform == null)
        {
            Camera temp = GameManager.Instance.GetPlayerCamera();
            if (temp != null) this.playerCamTransform = temp.transform;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
