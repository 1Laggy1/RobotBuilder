using UnityEngine;
using System.Collections.Generic;

public class Robot : MonoBehaviour
{
    private Dictionary<PartType, GameObject> spawnedParts = new Dictionary<PartType, GameObject>();
    [SerializeField] 
    RobotPart_SO[] defaultParts;

    void Start()
    {
        RespawnPart(defaultParts);
    }

    void RespawnPart(RobotPart_SO[] part)
    {
        if (spawnedParts.Count > 0)
        {
            foreach(var spawnedPart in spawnedParts)
            {
                Destroy(spawnedPart.Value);
            }
            spawnedParts.Clear();
        }
        
        foreach(RobotPart_SO part_SO in part)
        {
            if (spawnedParts.ContainsKey(part_SO.partType))
            {
                Debug.LogError("Only one of each part type can be in the robot");
                return;
            }
            GameObject spawnedPart = Instantiate(part_SO.prefab, this.transform);
            spawnedParts.Add(part_SO.partType, spawnedPart);
        }
        TorsoAddOffset();
    }
    public void RespawnPart(RobotPart_SO part)
    {
        Destroy(spawnedParts[part.partType]);
        Instantiate(part.prefab, this.transform);
        TorsoAddOffset();
    }
    void TorsoAddOffset()
    {
        TorsoPart torsoPart = spawnedParts[PartType.Torso].GetComponent<TorsoPart>();
        foreach(var part in spawnedParts)
        {
            FromTorsoPart fromTorsoPart = part.Value.GetComponent<FromTorsoPart>();
            if (fromTorsoPart)
            {
                torsoPart.AddOffsetToPart(fromTorsoPart);
            }
        }
    }
}
