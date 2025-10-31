
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroProgression", menuName = "Create Hero Progression", order = 1)]
public class HeroProgression : ScriptableObject
{
    [SerializeField] StatProgression[] statsProgression;
    Dictionary<HeroStat, float[]> lookUpTable;

    [Serializable]
    struct StatProgression
    {
        public HeroStat stat;
        public float[] valueByLevel;
    }

    public float GetStat(HeroStat stat, int level)
    {
        if (lookUpTable == null) BuildLookUpTable();
        if (level < 0) return -1;
        if (!lookUpTable.ContainsKey(stat)) return -1;

        float[] statValueByLevels = lookUpTable[stat];
        if (statValueByLevels.Length < level)
        {
            return -1;
        }

        return lookUpTable[stat][level - 1];
    }

    private void BuildLookUpTable()
    {
        if (lookUpTable != null) return;

        lookUpTable = new Dictionary<HeroStat, float[]>();

        foreach (StatProgression statProgression in statsProgression)
        {
            lookUpTable.Add(statProgression.stat, statProgression.valueByLevel);
        }
    }
}