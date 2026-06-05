using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PartSocketMapping
{
    public PartType partType;
    public Transform socketTransform;
}

public class TorsoPart : RobotPart
{
    [Header("Sockets for other parts")]
    [SerializeField] 
    PartSocketMapping[] socketMappings;
    Dictionary<PartType, Transform> socketDictionary;
    bool isDictionary = false;
    public void AddOffsetToPart(FromTorsoPart part)
    {
        if (!isDictionary)
        {
            InitializeDictionary();
        }
        if (socketDictionary.ContainsKey(part.robotPart_SO.partType))
        {
            part.AddOffset(socketDictionary[part.robotPart_SO.partType].position);
        }
    }
    void InitializeDictionary()
    {
        isDictionary = true;
        socketDictionary = new Dictionary<PartType, Transform>();

        foreach (var mapping in socketMappings)
        {
            if (!socketDictionary.ContainsKey(mapping.partType))
            {
                socketDictionary.Add(mapping.partType, mapping.socketTransform);
            }
        }
    }
}
