using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Class : MonoBehaviour
{
    private string[] firstNames = { "John", "Kenny", "Jane"};
    private string[] lastNames = { "Smith", "Rogers", "Doe"};

    private void Start()
    {

    }
}

public class NPC
{
    public bool IsSelected {get; set;}
    public string Name { get; set; }
    public int Age { get; set; }
    public string LeadershipStyle { get; set; }
    public float TechSkill { get; set; }
    public float SocialSkill { get; set; }
    public float WorkEfficiency { get; set; }
    public string SocialPref { get; set; }
    public bool IsNeurodivergent { get; set; }
    public bool IsDisabled { get; set; }
    public bool InRelationship { get; set; }
    public int Salary { get; set; }
    public int Mood { get; set; }
    public float TaskValue { get; set; }
    public float WorkDone { get; set; } = 0;
}

