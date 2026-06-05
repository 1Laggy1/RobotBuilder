using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject robot_go;
    Robot robot;
    [SerializeField]
    Transform robot_position;

    [Header("Dropdown References")]
    [SerializeField] private TMP_Dropdown headDropdown;
    [SerializeField] private TMP_Dropdown torsoDropdown;
    [SerializeField] private TMP_Dropdown legsDropdown;

    [Header("Part list UI")]
    [SerializeField] private RobotPart_SO[] UI_heads;
    [SerializeField] private RobotPart_SO[] UI_torsos;
    [SerializeField] private RobotPart_SO[] UI_legs;
    [Header("Stats UI")]
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private TMP_Text powerText;
    [Header("Color Dropdowns UI")]
    [SerializeField] private TMP_Dropdown headColorDropdown;
    [SerializeField] private TMP_Dropdown torsoColorDropdown;
    [SerializeField] private TMP_Dropdown legsColorDropdown;

    public void UpdateStatsUI()
    {
        float totalWeight = 0;
        float totalPower = 0;

        if (UI_heads.Length > 0 && headDropdown.value < UI_heads.Length)
        {
            totalWeight += UI_heads[headDropdown.value].Weight;
            totalPower += UI_heads[headDropdown.value].Power;
        }

        if (UI_torsos.Length > 0 && torsoDropdown.value < UI_torsos.Length)
        {
            totalWeight += UI_torsos[torsoDropdown.value].Weight;
            totalPower += UI_torsos[torsoDropdown.value].Power;
        }

        if (UI_legs.Length > 0 && legsDropdown.value < UI_legs.Length)
        {
            totalWeight += UI_legs[legsDropdown.value].Weight;
            totalPower += UI_legs[legsDropdown.value].Power;
        }

        if (weightText != null) weightText.text = "Weight: " + totalWeight;
        if (powerText != null) powerText.text = "Power: " + totalPower;
    }

    public void UI_SelectPart(PartType type, int index)
    {
        switch (type)
        {
            case PartType.Head:
                if (index >= 0 && index < UI_heads.Length) 
                    robot.RespawnPart(UI_heads[index]);
                break;

            case PartType.Torso:
                if (index >= 0 && index < UI_torsos.Length) 
                    robot.RespawnPart(UI_torsos[index]);
                break;

            case PartType.Legs:
                if (index >= 0 && index < UI_legs.Length) 
                    robot.RespawnPart(UI_legs[index]);
                break;
        }
        UpdateStatsUI();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
        SetupDropdown(headDropdown, UI_heads);
        SetupDropdown(torsoDropdown, UI_torsos);
        SetupDropdown(legsDropdown, UI_legs);

     
        SpawnRobot();

    
        if (robot != null && robot.defaultParts != null)
        {
            foreach (var part in robot.defaultParts)
            {
                if (part == null) continue;

                
                if (part.partType == PartType.Head)
                    headDropdown.SetValueWithoutNotify(GetPartIndex(UI_heads, part));
                else if (part.partType == PartType.Torso)
                    torsoDropdown.SetValueWithoutNotify(GetPartIndex(UI_torsos, part));
                else if (part.partType == PartType.Legs)
                    legsDropdown.SetValueWithoutNotify(GetPartIndex(UI_legs, part));
            }
        }

    
        headDropdown.onValueChanged.AddListener(index => UI_SelectPart(PartType.Head, index));
        torsoDropdown.onValueChanged.AddListener(index => UI_SelectPart(PartType.Torso, index));
        legsDropdown.onValueChanged.AddListener(index => UI_SelectPart(PartType.Legs, index));
        SetupColorDropdown(headColorDropdown);
        SetupColorDropdown(torsoColorDropdown);
        SetupColorDropdown(legsColorDropdown);

        headColorDropdown.onValueChanged.AddListener(index => UI_SetPartColor(PartType.Head, index));
        torsoColorDropdown.onValueChanged.AddListener(index => UI_SetPartColor(PartType.Torso, index));
        legsColorDropdown.onValueChanged.AddListener(index => UI_SetPartColor(PartType.Legs, index));
        UpdateStatsUI();
    }

    
    public void UI_PlayAction()
    {
        if (robot != null)
        {
            robot.PlayAllAnimations();
        }
    }

private void SetupColorDropdown(TMP_Dropdown dropdown)
    {
        dropdown.ClearOptions();
        
        dropdown.AddOptions(new List<string> { "White", "Red", "Green", "Blue", "Yellow" });
    }

    public void UI_SetPartColor(PartType type, int colorIndex)
    {
        if (robot == null) return;

        Color chosenColor = Color.white;
        switch (colorIndex)
        {
            case 0: chosenColor = Color.white; break;
            case 1: chosenColor = Color.red; break;
            case 2: chosenColor = Color.green; break;
            case 3: chosenColor = Color.blue; break;
            case 4: chosenColor = Color.yellow; break;
        }

        robot.ChangePartColor(type, chosenColor);
    }

    private int GetPartIndex(RobotPart_SO[] array, RobotPart_SO part)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == part) return i;
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupDropdown(TMP_Dropdown dropdown, RobotPart_SO[] partsArray)
    {
        dropdown.ClearOptions();
        
        List<string> optionsNames = new List<string>();
        
        foreach (var part in partsArray)
        {
            if (part != null)
            {
                optionsNames.Add(part.name); 
            }
        }
        
        dropdown.AddOptions(optionsNames);
    }

    void SpawnRobot()
    {
        if (robot_go && robot_position)
        robot = Instantiate(robot_go, robot_position.position, robot_position.rotation).GetComponent<Robot>();
    }
}
