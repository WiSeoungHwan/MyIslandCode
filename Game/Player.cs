using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Ground ground;
    [SerializeField]
    GameObject[] practicableAreas;
    // Use this for initialization
    void Start()
    {
		checkIsPracticableArea();
    }

    private void move()
    {
        // click the ground 
        if (Input.GetMouseButtonDown(0))
        {
            // get hitInfo
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // differanciate
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.tag == "PracticableArea")
                {
                    var pos = hitInfo.transform.position;
                    
                }
            }
        }
    }

    private void checkIsPracticableArea()
    {
        foreach (var i in practicableAreas)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
