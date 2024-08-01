using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSet
{
    public string SetName { get; set; }
    public int Cost { get; set; }
    public bool IsPurchased { get;  set; }
    public int MoodBonus { get; set; }
    public int WorkBonus { get; set; }
    //public string PrerequisiteSet { get; private set; }

    public UpgradeSet(string setName, int cost, int moodBonus, int workBonus, string prerequisiteSet = null)
    {
        SetName = setName;
        Cost = cost;
        MoodBonus = moodBonus;
        WorkBonus = workBonus;
        //PrerequisiteSet = prerequisiteSet;
        IsPurchased = false;
    }

    public void Purchase()
    {
        IsPurchased = true;
    }
}