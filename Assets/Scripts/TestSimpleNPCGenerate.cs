using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleNPCGenerator : MonoBehaviour
{
    public Text npcInfoOutput; // Reference to the UI Text component to display NPC info
    public Button generateButton; // Reference to the UI Button component

    private string[] firstNames = { "Worker" };
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

    public NPC GenerateRandomNPC()
    {
        NPC newNPC = new NPC
        {
            Name = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)],
            Age = Random.Range(20, 61),
            WorkEfficiency = Random.Range(1f, 10f),
            Mood = Random.Range(-5, 6),
        };

        newNPC.Salary = CalculateSalary(newNPC.Age, newNPC.WorkEfficiency);

        return newNPC;
        //return new NPC(name, age, leadershipStyle, techSkill, socialSkill, workEfficiency, socialPref, isNeurodivergent, isDisabled, inRelationship, mood);
    }

    private int CalculateSalary(int age, float workEfficiency)
    {
        //return Mathf.RoundToInt(Random.Range(6000f, 8001f) + (age * (workEfficiency * 10)));
        return Mathf.RoundToInt(Random.Range(6000f, 6001f) + (age * (workEfficiency * 10)));
    }

    private void DisplayNPCInfo(NPC npc)
    {
        npcInfoOutput.text = $"Name: {npc.Name}\nWork Efficiency: {npc.WorkEfficiency}\nSalary: {npc.Salary}\nMood: {npc.Mood}";
    }
}

public class SimpleNPC_ToGenerate
{
    public string Name { get; set; }
    public int Age { get; set; }
    public float WorkEfficiency { get; set; }
    public int Salary { get; set; }
    public int Mood { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}\nAge: {Age}\nWork Efficiency: {WorkEfficiency}\nSalary: {Salary}\nMood: {Mood}";
    }
}
