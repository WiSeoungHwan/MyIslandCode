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
                    if (ground.posArr.Contains(new Vector3(pos.x, 0, pos.z)))
                    {
                        transform.position = new Vector3(pos.x, 0, pos.z);
						checkIsPracticableArea();
                    }

                }
            }
        }
    }

    private void checkIsPracticableArea()
    {
        foreach (var i in practicableAreas)
        {
            if (ground.posArr.Contains(new Vector3(i.transform.position.x, 0, i.transform.position.z)))
            {
                i.SetActive(true);
            }
            else
            {
                i.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
