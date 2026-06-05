using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject robot_go;
    Robot robot;
    [SerializeField]
    Transform robot_position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnRobot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRobot()
    {
        if (robot_go && robot_position)
        robot = Instantiate(robot_go, robot_position.position, robot_position.rotation).GetComponent<Robot>();
    }
}
