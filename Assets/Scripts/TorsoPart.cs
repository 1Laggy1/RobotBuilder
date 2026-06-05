using UnityEngine;

public class TorsoPart : RobotPart
{
    [Header("Sockets for other parts")]
    [SerializeField] 
    public Transform headSocket;
    [SerializeField] 
    public Transform legsSocket;
}
