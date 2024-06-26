using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Class : MonoBehaviour
{
    private string[] firstNames = { "John", "Kenny", "Jane"};
    private string[] lastNames = { "Smith", "Rogers", "Doe"};

    private void Start()
    {
        NPC npc = GenerateRandomNPC();
        Debug.Log(npc.ToString()); // Print details to console
    }

    private NPC GenerateRandomNPC()
    {
        NPC newNPC = new NPC
        {
            Name = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)],
            Age = Random.Range(20, 61),
            LeadershipStyle = Random.Range(0, 2) == 0 ? "autocratic" : "democratic",
            TechSkill = Random.Range(1f, 6f),
            SocialSkill = Random.Range(1f, 6f),
            WorkEfficiency = Random.Range(1f, 11f),
            SocialPref = Random.Range(0, 2) == 0 ? "introvert" : "extrovert",
            IsNeurodivergent = Random.Range(0, 2) == 0,
            IsDisabled = Random.Range(0, 2) == 0,
            InRelationship = Random.Range(0, 2) == 0,
            Mood = Random.Range(-5, 6),
        };

        newNPC.Salary = CalculateSalary(newNPC.Age, newNPC.WorkEfficiency);

        return newNPC;
    }

    private int CalculateSalary(int age, float workEfficiency)
    {
        return Mathf.RoundToInt(Random.Range(5000f, 8000f) + (age * (workEfficiency * 10)));
    }
}

public class NPC
{
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

    // public override string ToString()
    // {
    //     return $"Name: {Name}\nAge: {Age}\nLeadershipStyle: {LeadershipStyle}\nTechSkill: {TechSkill}\nSocialSkill: {SocialSkill}\nWorkEfficiency: {WorkEfficiency}\nSocialPref: {SocialPref}\nIsNeurodivergent: {IsNeurodivergent}\nIsDisabled: {IsDisabled}\nInRelationship: {InRelationship}\nSalary: {Salary}";
    // }
}

