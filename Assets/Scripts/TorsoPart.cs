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
        
        PartType type = part.robotPart_SO.partType; 
        
        if (socketDictionary.ContainsKey(type))
        {
            Transform socket = socketDictionary[type];
            part.transform.SetParent(socket);
            
            part.transform.localPosition = Vector3.zero; 
            
            part.AddOffset(socket.localPosition); 
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
