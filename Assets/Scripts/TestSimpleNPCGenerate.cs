using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleNPCGenerator : MonoBehaviour
{
    // Text Output Fields
    public Text npcNameOutput; // Name
    public Text npcAgeOutput; // Age
    public Text npcWorkEfficiencyOutput; // Work Efficiency
    public Text npcSalaryOutput; // Salary
    public Text npcMoodOutput; // Mood
    public Text npcInfoOutput; // Whole info
    public Slider npcMoodSlider;
    public Button generateButton; // Debug Generate NPC Button


    // 3D Model reference
    public GameObject[] npcPrefabs; // Array of different 3D model prefabs
    public Transform[] spawnPoints; // Array of spawn points for NPCs


    private string[] firstNames = { "Worker" };
    private string[] lastNames = { "Smith", "Johnson", "Brown", "Williams", "Jones", "Garcia", "Miller"};

    private void Start()
    {
        // Debug NPC Spawn
        /* if (spawnPoint == null)
        {
            Debug.LogWarning("Spawn point is not set. Creating a default spawn point.");
            //GameObject defaultSpawnPoint = new GameObject("DefaultSpawnPoint");
            //spawnPoint = defaultSpawnPoint.transform;
            //spawnPoint.position = new Vector3(0, 0, 0); // Adjust this position as needed
        }
        else {
            Debug.LogWarning("Spawn point has been set.");
        } */
        generateButton.onClick.AddListener(OnGenerateButtonClick); 
    }

    private void OnGenerateButtonClick()
    {
        for (int i = 0; i < 6; i++)
        {
            NPC newNPC = GenerateRandomNPC();
            DisplayNPCInfo(newNPC); // Optionally display the first NPC's info
            SpawnNPCModel(newNPC, i);
        }
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

    public void DisplayNPCInfo(NPC npc)
    {
        npcNameOutput.text = $"{npc.Name}";
        //npcAgeOutput.text = $"{npc.Age}";
        //npcWorkEfficiencyOutput.text = $"{npc.WorkEfficiency}";
        npcSalaryOutput.text = $"{npc.Salary} / month";
        npcMoodOutput.text = $"{npc.Mood}";
        npcMoodSlider.value = npc.Mood;
        npcInfoOutput.text = $"Name: {npc.Name}\nWork Efficiency: {npc.WorkEfficiency}\nSalary: {npc.Salary}\nMood: {npc.Mood}";
        //Debug.Log("Name: " + npc.Name + "\nAge: " + npc.Age);
    }

    private void SpawnNPCModel(NPC npc, int index)
    {
        if (npcPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("NPC Prefabs or Spawn Points are not assigned.");
            return;
        }

        GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        Transform spawnPoint = spawnPoints[index % spawnPoints.Length];

        GameObject npcModel = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
        if (npcModel != null)
        {
            Debug.Log($"Spawned NPC Model at {spawnPoint.position}");
            NPCModel npcModelScript = npcModel.GetComponent<NPCModel>();
            if (npcModelScript != null)
            {
                npcModelScript.Initialize(npc, this);
            }
            else
            {
                Debug.LogError("NPCModel script is missing on the prefab.");
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate NPC Model.");
        }
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
public class NPCModel : MonoBehaviour
{
    private NPC npc;
    private SimpleNPCGenerator npcGenerator;

    public void Initialize(NPC npc, SimpleNPCGenerator generator)
    {
        this.npc = npc;
        this.npcGenerator = generator;
    }

    private void OnMouseDown()
    {
        npcGenerator.DisplayNPCInfo(npc);
    }
}