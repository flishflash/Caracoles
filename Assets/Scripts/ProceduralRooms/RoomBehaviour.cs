using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] walls; 
    
    List<GameObject> roomFloors = new List<GameObject>();
    List<GameObject> placedWalls = new List<GameObject>();

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Began;

    [SerializeField] GameObject currentObjToPlace;
    [SerializeField] GridManager gridManager;

    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.transform.tag == "Tile" && (!hit.transform.GetComponent<Tile>().isOccuped 
                    && hit.transform.GetComponent<Tile>().isAccesible))
                {
                    AddFloorToList(Instantiate(currentObjToPlace, hit.transform.position, Quaternion.identity));
                    hit.transform.GetComponent<Tile>().isOccuped = true;
                    gridManager.SetAllToNotAccesible();
                    CheckNearTiles();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.transform.tag == "Tile" && (!hit.transform.GetComponent<Tile>().isOccuped
                && hit.transform.GetComponent<Tile>().isAccesible))
            {
                GameObject go = Instantiate(currentObjToPlace, hit.transform.position, Quaternion.identity);
                AddFloorToList(go);
                go.transform.parent = transform;
                hit.transform.GetComponent<Tile>().isOccuped = true;
                gridManager.SetAllToNotAccesible();
                CheckNearTiles();
            }
        }
    }

    void CheckNearTiles()
    {
        foreach (var currentTile in gridManager.tiles)
        {
            if (currentTile.isOccuped)
            {
                RayCastToEnableTile(currentTile.transform.position, Vector3.forward);
                RayCastToEnableTile(currentTile.transform.position, Vector3.back);
                RayCastToEnableTile(currentTile.transform.position, Vector3.left);
                RayCastToEnableTile(currentTile.transform.position, Vector3.right);
            }
        }
    }

    void RayCastToEnableTile(Vector3 pos, Vector3 direction)
    {
        Ray ray = new Ray(pos, direction); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.transform.tag == "Tile")
            {
                hit.transform.GetComponent<Tile>().isAccesible = true;
            }
        }
    }

    public void AddFloorToList(GameObject go)
    {
        roomFloors.Add(go);
        CheckToIntanceWall();
    }

    void CheckToIntanceWall()
    {
        for (int i = 0; i < placedWalls.Count; i++)
        {
            Destroy(placedWalls[i]);
        }
        placedWalls.Clear();
        placedWalls.TrimExcess();
        foreach (var floor in roomFloors)
        {
            int i;
            if (!CheckFrontFloor(floor))
            {
                i = Random.Range(0, walls.Length);
                placedWalls.Add(Instantiate(walls[i], floor.transform.position + (Vector3.forward / 1.5f) + (Vector3.up / 5), Quaternion.identity));
            }
            if(!CheckBackFloor(floor))
            {
                i = Random.Range(0, walls.Length);
                placedWalls.Add(Instantiate(walls[i], floor.transform.position + (Vector3.back / 1.5f) + (Vector3.up / 5), Quaternion.identity));
            }
            if(!CheckLeftFloor(floor))
            {
                i = Random.Range(0, walls.Length);
                placedWalls.Add(Instantiate(walls[i], floor.transform.position + (Vector3.left / 1.5f) + (Vector3.up / 5), Quaternion.Euler(0,90,0)));
            }
            if(!CheckRightFloor(floor))
            {
                i = Random.Range(0, walls.Length);
                placedWalls.Add(Instantiate(walls[i], floor.transform.position + (Vector3.right / 1.5f) + (Vector3.up / 5), Quaternion.Euler(0, 90, 0)));
            }
        }
        for (int i = 0; i < placedWalls.Count; i++)
        {
            placedWalls[i].transform.parent=transform;
        }
    }

    bool CheckFrontFloor(GameObject go)
    {
        bool ret = false;

        Ray ray = new Ray(go.transform.position + (Vector3.up / 10), Vector3.forward); ;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && Vector3.Distance(hit.transform.position, go.transform.position) <= 1.5f)
            {
                return true;
            }
        }
        
        return ret;
    }

    bool CheckBackFloor(GameObject go)
    {
        bool ret = false;

        Ray ray = new Ray(go.transform.position + (Vector3.up / 10), Vector3.back); ;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && Vector3.Distance(hit.transform.position, go.transform.position) <= 1.5f)
            {
                return true;
            }
        }

        return ret;
    }

    bool CheckLeftFloor(GameObject go)
    {
        bool ret = false;

        Ray ray = new Ray(go.transform.position + (Vector3.up / 10), Vector3.left); ;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && Vector3.Distance(hit.transform.position, go.transform.position) <= 1.5f)
            {
                return true;
            }
        }

        return ret;
    }

    bool CheckRightFloor(GameObject go)
    {
        bool ret = false;

        Ray ray = new Ray(go.transform.position + (Vector3.up / 10), Vector3.right); ;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && Vector3.Distance(hit.transform.position, go.transform.position) <= 1.5f)
            {
                return true;
            }
        }

        return ret;
    }
}
