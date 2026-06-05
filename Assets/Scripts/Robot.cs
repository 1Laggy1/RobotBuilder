using UnityEngine;
using System.Collections.Generic;

public class Robot : MonoBehaviour
{
    private Dictionary<PartType, GameObject> spawnedParts = new Dictionary<PartType, GameObject>();
    public RobotPart_SO[] defaultParts;
    private Dictionary<PartType, Color> partColors = new Dictionary<PartType, Color>()
    {
        { PartType.Head, Color.white },
        { PartType.Torso, Color.white },
        { PartType.Legs, Color.white }
    };

    // 2. Додай цей метод в будь-яке місце класу:
    public void ChangePartColor(PartType type, Color color)
    {
        partColors[type] = color; // Запам'ятовуємо колір для цього типу
        ApplyColorToPart(type);   // Фарбуємо поточну деталь
    }

    private void ApplyColorToPart(PartType type)
    {
        if (spawnedParts.ContainsKey(type) && spawnedParts[type] != null)
        {
            
            RobotPart currentPartScript = spawnedParts[type].GetComponent<RobotPart>();
            
            Renderer[] renderers = spawnedParts[type].GetComponentsInChildren<Renderer>(true);
            Color targetColor = partColors[type];

            foreach (Renderer rend in renderers)
            {
                // Знаходимо, до якої саме деталі належить цей рендерер (шукає найближчий скрипт вгору)
                RobotPart ownerPart = rend.GetComponentInParent<RobotPart>();
                
                // Якщо рендерер належить саме ЦІЙ деталі (наприклад, Торсу), а не прикріпленій до неї Голові — фарбуємо
                if (ownerPart == currentPartScript)
                {
                    rend.material.color = targetColor;
                }
            }
        }
    }

    public void PlayAllAnimations()
    {
        foreach (var partObj in spawnedParts.Values)
        {
            if (partObj != null)
            {
                RobotPart partScript = partObj.GetComponent<RobotPart>();
                if (partScript != null)
                {
                    partScript.PlayActionAnim();
                }
            }
        }
    }

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
        if (spawnedParts.ContainsKey(part.partType))
        {
            if (part.partType == PartType.Torso)
            {
                foreach (var pair in spawnedParts)
                {
                    if (pair.Key != PartType.Torso && pair.Value != null)
                    {
                        pair.Value.transform.SetParent(this.transform); 
                    }
                }
            }

            if (spawnedParts[part.partType] != null)
            {
                Destroy(spawnedParts[part.partType]);
            }
        }

        GameObject spawnedPart = Instantiate(part.prefab, this.transform);
        
        spawnedParts[part.partType] = spawnedPart;
        
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
        ApplyColorToPart(PartType.Head);
        ApplyColorToPart(PartType.Torso);
        ApplyColorToPart(PartType.Legs);
    }
}
