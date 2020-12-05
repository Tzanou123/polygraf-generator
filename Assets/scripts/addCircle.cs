using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class addCircle : MonoBehaviour
{
    public Button yourButton;
    public GameObject clone;
    public GameObject toClone;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        GameObject circle = Instantiate(toClone, clone.transform);
        circle.name = "Circle";
    }

}
