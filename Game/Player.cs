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
            foreach (Tile tile in ground.tileArr){
                var iVec = i.transform.position;
                var tileVec = tile.transform.position;
                Vector3 iVector = new Vector3(iVec.x,0,iVec.z);
                Vector3 tileVector = new Vector3(tileVec.x,0,tileVec.z);
                if (iVector == tileVector){
                    i.SetActive(true);
                }else{
                    i.SetActive(false);
                }
            }
        }
    }

    
    void Update()
    {
        move();
    }
}
