using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Cell_01 : MonoBehaviour {

    float rowNumber;
    float columnNumber;

    public int rN;
    public int cN;

    //public Material master;

    public Renderer cellRenderer;

    private void Start()
    {
        rowNumber = (float)rN;
        columnNumber = (float)cN;

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        mpb.SetFloat("_RowNumber", rowNumber);
        mpb.SetFloat("_ColumnNumber", columnNumber);

        cellRenderer.SetPropertyBlock(mpb);
    }

    public void ChangeNumberDisplay() {
        rowNumber = (float)rN;
        columnNumber = (float)cN;
                      
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
               
        mpb.SetFloat("_RowNumber", rowNumber);
        mpb.SetFloat("_ColumnNumber", columnNumber);

        cellRenderer.SetPropertyBlock(mpb);
               
    }
    
    public int totalRows() {
        return (int)cellRenderer.sharedMaterial.GetFloat("_TotalRowNumber");
    }

    public int totalColumns() {
        return (int)cellRenderer.sharedMaterial.GetFloat("_TotalColumnNumber");
    }

}
