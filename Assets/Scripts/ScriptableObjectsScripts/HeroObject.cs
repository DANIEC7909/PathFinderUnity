using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObjects/Hero", order = 1)]
public class HeroObject : ScriptableObject
{
    public int HeroID;
    public Sprite HeroImage;
    public string HeroName;
    public HeroStatistics HeroMaxStatistics;
    public Material HeroSkin;
    public string PrefabKey;

}
