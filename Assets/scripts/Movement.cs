using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1.5f;
    private Vector3 target;
    private Boolean canMove = false;

    private globalVar globalVar;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        globalVar = GameObject.FindWithTag("MainCamera").GetComponent<globalVar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
        }
        if (Input.GetMouseButton(0) && globalVar.isInBezier == false)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (!Input.GetKey(KeyCode.LeftShift) && ((hit.collider != null && hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()) || canMove == true))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = (pos);
                canMove = true;
            }

        }
        // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
