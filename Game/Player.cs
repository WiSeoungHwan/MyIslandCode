using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    public delegate void PlayerDelegate();

    [SerializeField]
    Ground ground;
    [SerializeField]
    GameObject[] practicableAreas;

    public bool isBuildingMode = false;
    public bool isBuildingSample = false;
    public PlayerDelegate playerDel;

    public List<GameObject> sampleBuildings = new List<GameObject>(); 
    protected override void OnStart(){
        checkIsPracticableArea();
        playerDel = new PlayerDelegate(checkIsPracticableArea);
    }

    private void move(){
        
        // click the ground 
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.Instance.gameTrigger){Debug.Log("gameTrigger is false");return;}
            // get hitInfo
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.tag == "PracticableArea")
                {
                    Debug.Log("Suc");
                    var pos = hitInfo.transform.position;
                    gameObject.transform.position = new Vector3(pos.x,transform.position.y,pos.z);
                    checkIsPracticableArea();
                }
                if (hitInfo.transform.tag == "SampleBuilding"){
                    var tile = hitInfo.transform.parent.GetComponent<Tile>();
                    buildingTower(tile, false);
                    foreach(GameObject sample in sampleBuildings){
                        sample.SetActive(false);
                    }
                    isBuildingMode = false;
                    isBuildingSample = false;
                    checkIsPracticableArea();
                }
            }
        }
    }

    private void checkIsPracticableArea(){
        foreach (GameObject i in practicableAreas){
            i.SetActive(false);
            foreach (Tile tile in ground.tileArr){
                var iVec = i.transform.position;
                var tileVec = tile.transform.position;
                Vector3 iVector = new Vector3(iVec.x,0,iVec.z);
                Vector3 tileVector = new Vector3(tileVec.x,0,tileVec.z);
                
                if (iVector == tileVector) {
                    switch (tile.state) {
                        case TileState.normal:
                            i.SetActive(true);
                            if (isBuildingMode){
                                buildingTower(tile, true);
                            }else{
                                
                            }
                            break;
                        case TileState.material:
                            
                            break;
                        case TileState.building:
                            break;
                    }
                }
            }
        }
        isBuildingMode = false;
    }

    private void buildingTower(Tile tile, bool isSample){
        string buildingName = isSample ? "sampleBuilding" : "building_1";
        
        if (Resources.Load("Prefab/Tile/Building/"+buildingName) == null){Debug.Log(buildingName+" is null"); return;}
        Vector3 pos = new Vector3(tile.transform.position.x,0.0f,tile.transform.position.z);
        GameObject tower = Instantiate(Resources.Load("Prefab/Tile/Building/"+buildingName),pos, Quaternion.identity) as GameObject;
        tower.transform.SetParent(tile.transform);
        tower.tag = isSample ? "SampleBuilding" : "Building";
        tile.state = isSample ? TileState.normal : TileState.building;
        if (isSample) {sampleBuildings.Add(tower);}
        tower.SetActive(true);
    }


    void Update()
    {
        move();
    }
}
