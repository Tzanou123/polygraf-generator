using System;
using UnityEngine;

public class BezierScript : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private globalVar globalVar;

    private Transform parent;

    public Transform rememberTo;
    public GameObject arrowToClone;

    void Start()
    {
        rememberTo = null;
        lineRenderer = GetComponent<LineRenderer>();
        globalVar = GameObject.FindWithTag("MainCamera").GetComponent<globalVar>();
        parent = transform.parent;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector3(0, 0, 0);
        transform.position = Camera.main.ScreenToWorldPoint(parent.position);
        globalVar.uidLineDrawCircle = gameObject.GetInstanceID();
        DrawQuadraticBezierCurve(
                    mousePos2D,
                    mousePos2D,
                    new Vector3(parent.position.x - transform.parent.position.x, parent.position.y - transform.parent.position.y, 0));
    }

    void Update()
    {
        transform.position = transform.parent.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        float angle = Vector3.Angle(transform.parent.position, mousePos);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector2 mousePos2D0 = new Vector2(0, 0);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        Vector3[] allPoints = new Vector3[200];
        lineRenderer.GetPositions(allPoints);
        if (Input.GetMouseButtonUp(0) && globalVar.uidLineDrawCircle == gameObject.GetInstanceID())
        {
            if ((hit.collider != null && hit.collider.gameObject.name == "Circle"))
            {
                rememberTo = hit.collider.gameObject.transform;
                DrawQuadraticBezierCurve(
                    mousePos2D0,
                    allPoints[99],
                    new Vector3(rememberTo.position.x - transform.parent.position.x, rememberTo.position.y - transform.parent.position.y, 0));

                GameObject arrow = Instantiate(arrowToClone, transform.parent.parent);
                arrow.name = "Arrow";
                arrow.transform.SetParent(transform);
                placeArrow(arrow, allPoints[99]);
            }
            else
            {
                Destroy(gameObject);
            }

            globalVar.isInLineDraw = false;
            globalVar.uidLineDrawCircle = -1;
            globalVar.circleSelected = null;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

            if (Input.GetMouseButton(0))
            {
                if (hit.collider != null && globalVar.circleSelected == null)
                {
                    globalVar.circleSelected = hit.collider.gameObject;
                }

                if (
                    globalVar.circleSelected.GetInstanceID() == transform.parent.gameObject.GetInstanceID() &&
                    (
                        globalVar.isInLineDraw == false ||
                        globalVar.uidLineDrawCircle == -1 ||
                        globalVar.uidLineDrawCircle == gameObject.GetInstanceID()
                        )
                    )
                {
                    // transform.parent = null;
                    globalVar.isInLineDraw = true;
                    globalVar.uidLineDrawCircle = gameObject.GetInstanceID();
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos2D = new Vector3(mousePos.x, mousePos.y, 0);
                    float v = Vector3.Distance(transform.parent.position, mousePos2D);
                    DrawQuadraticBezierCurve(
                        mousePos2D0,
                        allPoints[99],
                        new Vector3(mousePos.x - transform.parent.position.x, mousePos.y - transform.parent.position.y, 0)
                    );


                    // // convert mouse position into world coordinates
                    // Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // // get direction you want to point at
                    // Vector2 direction = (mouseWorldPosition - (Vector2)arrow.transform.position).normalized;

                    // // set vector of transform directly
                    // arrow.transform.up = direction;
                    Debug.Log(v);
                }
            }
        }
        if (rememberTo != null)
        {
            DrawQuadraticBezierCurve(
                mousePos2D0,
                allPoints[99],
                new Vector3(rememberTo.position.x - transform.parent.position.x, rememberTo.position.y - transform.parent.position.y, 0)
            );
        }
        if (transform.Find("Arrow"))
        {
            placeArrow(transform.Find("Arrow").gameObject, allPoints[99]);
        }
    }

    public void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
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

    Vector3 GetPoint(Vector3 pos1, Vector3 pos2, Int32 offset)
    {
        //get the direction between the two transforms -->
        Vector3 dir = (pos2 - pos1).normalized;

        //get a direction that crosses our [dir] direction
        //NOTE! : this can be any of a buhgillion directions that cross our [dir] in 3D space
        //To alter which direction we're crossing in, assign another directional value to the 2nd parameter
        Vector3 perpDir = Vector3.Cross(dir, Vector3.right);

        //get our midway point
        Vector3 midPoint = (pos1 + pos2) / 2f;

        //get the offset point
        //This is the point you're looking for.
        Vector3 offsetPoint = midPoint + (perpDir * offset);

        return offsetPoint;
    }

    public Vector3 CenterOfVectors(Vector3 OtherObjectA, Vector3 OtherObjectB, Vector3 OtherObjectC)
    {
        Vector3 directionCtoA = OtherObjectA - OtherObjectC; // directionCtoA = positionA - positionC
        Vector3 directionCtoB = OtherObjectB - OtherObjectC; // directionCtoB = positionB - positionC
        Vector3 midpointAtoB = new Vector3((directionCtoA.x + directionCtoB.x) / 2.0f, (directionCtoA.y + directionCtoB.y) / 2.0f, (directionCtoA.z + directionCtoB.z) / 2.0f); // midpoint between A B this is what you want
        return midpointAtoB;
    }

    public void placeArrow(GameObject arrow, Vector3 allPoints)
    {
        Vector3[] allPointsIn = new Vector3[200];
        lineRenderer.GetPositions(allPointsIn);
        arrow.transform.localPosition = allPoints;
        Debug.Log(allPoints);
        // // convert mouse position into world coordinates
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // // get direction you want to point at
        Vector2 direction = ((Vector2)allPointsIn[199] - (Vector2)((allPointsIn[199] - allPointsIn[0]) / 2)).normalized;
        // // set vector of transform directly
        arrow.transform.up = direction;
    }
}