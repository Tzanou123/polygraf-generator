using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalVar : MonoBehaviour
{
    public Boolean isInLineDraw;
    public Boolean isInBezier;

    public GameObject circleSelected;
    public Int32 uidLineDrawCircle;
    // Start is called before the first frame update
    void Start()
    {
        isInLineDraw = false;
        uidLineDrawCircle = -1;
        circleSelected = null;
        isInBezier = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
