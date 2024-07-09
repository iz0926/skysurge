using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapGenerator : MonoBehaviour
{
    public GameObject prevCeiling;
    public GameObject prevFloor;
    public GameObject ceiling;
    public GameObject floor;

    public GameObject player;

    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;
    public GameObject obstaclePrefab;
    public float minObstacleY;
    public float maxObstacleY;
    public float minObstacleSpacing;
    public float maxObstacleSpacing;
    public float minObstacleScaleY;
    public float maxObstacleScaleY;
    private float floorWidth;
    private float ceilingWidth;
    
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the width of the floor anc ceiling based on their sprite size
        floorWidth = floor.GetComponent<SpriteRenderer>().bounds.size.x;
        ceilingWidth = ceiling.GetComponent<SpriteRenderer>().bounds.size.x;
        
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // generate obstacles
        //obstacle1 = GenerateObstacle(player.transform.position.x + 10);
        //obstacle2 = GenerateObstacle(obstacle1.transform.position.x);
        //obstacle3 = GenerateObstacle(obstacle2.transform.position.x);
        //obstacle4 = GenerateObstacle(obstacle3.transform.position.x);
    }
     
    GameObject GenerateObstacle(float referenceX){
        GameObject obstacle = GameObject.Instantiate(obstaclePrefab);
        SetTransform(obstacle, referenceX);
        return obstacle;
    }

    void SetTransform(GameObject obstacle, float referenceX){
        obstacle.transform.position = new Vector3(referenceX + Random.Range(minObstacleSpacing, maxObstacleSpacing), Random.Range(minObstacleY, maxObstacleY), 0);
        obstacle.transform.localScale = new Vector3(obstacle.transform.localScale.x, Random.Range(minObstacleScaleY, maxObstacleY), obstacle.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        // 8.8 is the orthographic size of the camera, mainCamera.orthographicSize
        // calculate the horizontal extent of the camera view for an orthographic camera
        float cameraHorizontalExtent = 8 * Screen.width / Screen.height;
        float cameraRightEdge = mainCamera.transform.position.x + cameraHorizontalExtent;

        // trigger shift before the player reaches the edge of the screen
        if (player.transform.position.x > floor.transform.position.x + floorWidth - 4 * cameraHorizontalExtent){
            var tempCeiling = prevCeiling;
            var tempFloor = prevFloor;
            prevCeiling = ceiling;
            prevFloor = floor;
            // move tempCeiling and tempFloor to the end of the current ceiling and floor
            tempCeiling.transform.position = new Vector3(ceiling.transform.position.x + ceilingWidth, ceiling.transform.position.y, ceiling.transform.position.z);
            tempFloor.transform.position = new Vector3(floor.transform.position.x + floorWidth, floor.transform.position.y, floor.transform.position.z);
            ceiling = tempCeiling;
            floor = tempFloor;
        }

        // to keep generating obstacles
        if (player.transform.position.x > obstacle2.transform.position.x){
            var tempObstacle = obstacle1;
            obstacle1 = obstacle2;
            obstacle2 = obstacle3;
            obstacle3 = obstacle4;

            SetTransform(tempObstacle, obstacle3.transform.position.x);
            obstacle4 = tempObstacle;
        }
    }
}
