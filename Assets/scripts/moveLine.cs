using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLine : MonoBehaviour
{
    private globalVar globalVar;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    private Boolean isMoving = false;
    public GameObject toClone;
    public Boolean isArrowConfigOpen;
    void Start()
    {
        globalVar = GameObject.FindWithTag("MainCamera").GetComponent<globalVar>();
        lineRenderer = transform.parent.GetComponent<LineRenderer>();
        isArrowConfigOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonUp(0))
        // {
        //     isMoving = false;
        // }
        // if (Input.GetMouseButton(0))
        // {
        //     Vector2 mousePos2D0 = new Vector3(0, 0, 0);
        //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //     RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        //     if (isMoving == true || (hit.collider != null && hit.collider.gameObject.name == "Line"))
        //     {
        //         Debug.Log(hit.collider.gameObject.name);
        //         isMoving = true;
        //         // globalVar.isInLineDraw = true;
        //         // globalVar.uidLineDrawCircle = gameObject.GetInstanceID();
        //         gameObject.GetComponent<BezierScript>().rememberTo = null;
        //         DrawQuadraticBezierCurve(
        //             mousePos2D0,
        //             mousePos2D0,
        //             new Vector3(mousePos.x - transform.parent.position.x, mousePos.y - transform.parent.position.y, 0)
        //         );
        //     }
        // }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider.gameObject.name == "Arrow" && hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                Transform arrowConfig = transform.Find("ArrowConfig");
                isArrowConfigOpen = !isArrowConfigOpen;
                arrowConfig.gameObject.SetActive(isArrowConfigOpen);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
        {
            Vector2 mousePos2D0 = new Vector3(0, 0, 0);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (globalVar.isInLineDraw == false && (isMoving == true || (hit.collider != null && hit.collider.gameObject.name == "Arrow")))
            {
                Debug.Log("diid" + hit.collider.gameObject.name);
                isMoving = true;
                globalVar.isInLineDraw = true;
                globalVar.uidLineDrawCircle = transform.parent.parent.gameObject.GetInstanceID();
                globalVar.circleSelected = transform.parent.parent.gameObject;
                GameObject line = Instantiate(toClone, transform.parent.parent);
                line.name = "Line";
                Destroy(transform.parent.gameObject);
            }
        }
    }
    void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
    {
        lineRenderer.positionCount = 200;
        lineRenderer.useWorldSpace = false;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            lineRenderer.SetPosition(i, B);
            t += (1 / (float)lineRenderer.positionCount);
        }
    }
}
