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
    [SerializeField]
    Material normalMaterial;

    public bool isBuildingMode = false;
    public bool isBuildingSample = false;
    public PlayerDelegate playerDel;

    public int playerIndex = 0;

    public List<GameObject> sampleBuildings = new List<GameObject>(); 
    int materialCount = 0;
    protected override void OnStart(){
        
        checkAround();
        playerDel = new PlayerDelegate(checkAround);
    }

    private void playerAction(){
        
        // click the ground 
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.Instance.gameTrigger){Debug.Log("gameTrigger is false");return;}
            // get hitInfo
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                move(hitInfo);
                if (!isBuildingMode){
                    collect(hitInfo);
                }
                if(GameManager.Instance.activeCount == 0){return;}
                if (hitInfo.transform.tag == "SampleBuilding"){
                    Tile tile = hitInfo.transform.parent.GetComponent<Tile>();
                    if (materialCount < 2){
                        return;
                    }
                    materialCount -= 2;
                    UIManager.Instance.materialCountChange(materialCount);
                    buildingTower(tile, false);
                    foreach(GameObject sample in sampleBuildings){
                        sample.SetActive(false);
                    }
                    isBuildingMode = false;
                    isBuildingSample = false;
                    GameManager.Instance.activeCount--;
                    UIManager.Instance.moveCountChange(GameManager.Instance.activeCount);
                    checkAround();
                }
            }
        }
    }
    private void collect(RaycastHit hitInfo){
        if(GameManager.Instance.activeCount == 0){return;}
        if (hitInfo.transform.tag == "Material"){
            Tile tile = hitInfo.transform.gameObject.GetComponent<Tile>();
            if (tile.index == playerIndex + 5 || tile.index == playerIndex - 5 || tile.index == playerIndex + 1 || tile.index == playerIndex - 1){
                tile.resetTilePrefab(TileState.normal);
                materialCount++;
                UIManager.Instance.materialCountChange(materialCount);
                GameManager.Instance.activeCount--;
                UIManager.Instance.moveCountChange(GameManager.Instance.activeCount);
                checkAround();
            }else {
                Debug.Log(tile.index);
            }
        }
    }

    private void move(RaycastHit hitInfo){
        if(GameManager.Instance.activeCount == 0){return;}
        switch (hitInfo.transform.name){
                    case "X1":
                        playerIndex += 5; // down
                        break;
                    case "X-1":
                        playerIndex -= 5; // up 
                        break;
                    case "Z1":
                        playerIndex += 1; // left
                        break;
                    case "Z-1":
                        playerIndex -= 1; // right
                        break;
                }
                if (hitInfo.transform.tag == "PracticableArea"){
                    var pos = hitInfo.transform.position;
                    gameObject.transform.position = new Vector3(pos.x,transform.position.y,pos.z);
                    GameManager.Instance.activeCount--;
                    UIManager.Instance.moveCountChange(GameManager.Instance.activeCount);
                    checkAround();
                }
    }

    private void checkAround(){
        foreach (GameObject i in practicableAreas){
            int aroundIndex = 0;
            switch (i.transform.name){
                    case "X1":
                        aroundIndex = playerIndex + 5;
                        break;
                    case "X-1":
                        aroundIndex = playerIndex - 5;
                        break;
                    case "Z1":
                        if (playerIndex == 4 || (playerIndex - 4) % 5 == 0){ // right side
                            i.SetActive(false);
                            continue;
                        }
                        aroundIndex = playerIndex + 1;
                        break;
                    case "Z-1":
                        if (playerIndex == 0 || playerIndex % 5 == 0){ // left side
                            i.SetActive(false);
                            continue;
                        }
                        aroundIndex = playerIndex - 1;
                        break;
                    default:
                        Debug.Log("Tile not found");
                        break;
            }
            if (aroundIndex < 0 || aroundIndex > 24){  // start tile or end tile
                i.SetActive(false);
                continue;
            }
            Tile tile = ground.tileArr[aroundIndex];
            switch (tile.state){
                case TileState.normal:
                    i.SetActive(true);
                    i.GetComponent<MeshRenderer>().material = normalMaterial;
                    if (isBuildingMode){
                        if(materialCount < 2){
                            i.GetComponent<MeshRenderer>().material.color = Color.red;
                        }else{
                            i.GetComponent<MeshRenderer>().material = normalMaterial;
                        }
                        buildingTower(tile, true);
                    }
                    break;
                case TileState.material:
                    i.SetActive(true);
                    i.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;
                case TileState.building:
                    i.SetActive(false);
                    break;
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
        playerAction();
    }
}
