using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateLine : MonoBehaviour
{
    public GameObject toClone;

    private globalVar globalVar;
    // Start is called before the first frame update
    void Start()
    {
        globalVar = GameObject.FindWithTag("MainCamera").GetComponent<globalVar>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (globalVar.isInLineDraw == false && Input.GetMouseButtonDown(0) && ((hit.collider != null && hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())))
            {
                globalVar.isInLineDraw = true;
                globalVar.uidLineDrawCircle = gameObject.GetInstanceID();
                GameObject line = Instantiate(toClone, transform);
                line.name = "Line";
            }
            
        }
    }
}
