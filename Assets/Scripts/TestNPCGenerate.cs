using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCGenerator : MonoBehaviour
{
    public Text npcInfoOutput; // Reference to the UI Text component to display NPC info
    public Button generateButton; // Reference to the UI Button component

    private string[] firstNames = { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Riley", "Dakota", "Reese", "Skyler", "Quinn" };
    private string[] lastNames = { "Smith", "Johnson", "Brown", "Williams", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };

    private void Start()
    {
        generateButton.onClick.AddListener(OnGenerateButtonClick);
    }

    private void OnGenerateButtonClick()
    {
        NPC newNPC = GenerateRandomNPC();
        DisplayNPCInfo(newNPC);
    }

    private NPC GenerateRandomNPC()
    {
        NPC newNPC = new NPC
        {
            Name = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)],
            Age = Random.Range(20, 61),
            LeadershipStyle = Random.Range(0, 2) == 0 ? "autocratic" : "democratic",
            TechSkill = Random.Range(1f, 5f),
            SocialSkill = Random.Range(1f, 5f),
            WorkEfficiency = Random.Range(1f, 10f),
            SocialPref = Random.Range(0, 2) == 0 ? "introvert" : "extrovert",
            IsNeurodivergent = Random.Range(0, 2) == 0,
            IsDisabled = Random.Range(0, 2) == 0,
            InRelationship = Random.Range(0, 2) == 0,
        };

        newNPC.Salary = CalculateSalary(newNPC.Age, newNPC.WorkEfficiency);

        return newNPC;
    }

    private int CalculateSalary(int age, float workEfficiency)
    {
        return Mathf.RoundToInt(Random.Range(6000f, 8001f) + (age * (workEfficiency * 10)));
    }

    private void DisplayNPCInfo(NPC npc)
    {
        npcInfoOutput.text = $"Name: {npc.Name}\nAge: {npc.Age}\nLeadership Style: {npc.LeadershipStyle}\nTech Skill: {npc.TechSkill}\nSocial Skill: {npc.SocialSkill}\nWork Efficiency: {npc.WorkEfficiency}\nSocial Preference: {npc.SocialPref}\nIs Neurodivergent: {npc.IsNeurodivergent}\nIs Disabled: {npc.IsDisabled}\nIn Relationship: {npc.InRelationship}\nSalary: {npc.Salary}";
    }
}

public class NPC_ToGenerate
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

    public override string ToString()
    {
        return $"Name: {Name}\nAge: {Age}\nLeadership Style: {LeadershipStyle}\nTech Skill: {TechSkill}\nSocial Skill: {SocialSkill}\nWork Efficiency: {WorkEfficiency}\nSocial Preference: {SocialPref}\nIs Neurodivergent: {IsNeurodivergent}\nIs Disabled: {IsDisabled}\nIn Relationship: {InRelationship}\nSalary: {Salary}";
    }
}
