using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezierMovement : MonoBehaviour
{
    private Vector3 lastPosition;
    private globalVar globalVar;
    // Start is called before the first frame update
    void Start()
    {
        globalVar = GameObject.FindWithTag("MainCamera").GetComponent<globalVar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && globalVar.isInBezier == true)
        {
            globalVar.isInBezier = false;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Vector2 mousePos2D0 = new Vector3(0, 0, 0);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if ((hit.collider != null && hit.collider.gameObject.name == "Bezier") || globalVar.isInBezier == true)
            {
                globalVar.isInBezier = true;
                Transform line = transform.parent.parent.parent;
                LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
                Vector3[] allPoints = new Vector3[200];
                lineRenderer.GetPositions(allPoints);
                line.GetComponent<BezierScript>().DrawQuadraticBezierCurve(
                    allPoints[0],
                    mousePos2D,
                    allPoints[199]
                );
            }
        }
        else
        {
            lastPosition = transform.position;
        }
    }
}
