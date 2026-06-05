using UnityEngine;
using UnityEngine.UI;

public enum PartType { Head, Torso, Legs }

[CreateAssetMenu(fileName = "RobotPart_SO", menuName = "Scriptable Objects/RobotPart_SO")]
public class RobotPart_SO : ScriptableObject
{
    public PartType partType;
    [Header("Visuals")]
    public string partName;
    public GameObject prefab;
    public Image PartUIImage;
    [Header("Stats")]
    public float Weight;
    public float Power;
}
