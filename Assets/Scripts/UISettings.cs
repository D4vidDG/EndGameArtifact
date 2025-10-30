using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "New UISettings", order = 1)]
public class UISettings : ScriptableObject
{
    [SerializeField] UIInfoByRarity[] uiInfoByRarity;
    [SerializeField] UIInfoByClass[] uiInfoByClass;

    UI_Dictionary<HeroRarity, RarityUIInfo> uiInfoByRarityDict;
    UI_Dictionary<HeroClass, ClassUIInfo> uiInfoByClassDict;

    void OnEnable()
    {
        uiInfoByRarityDict = new UI_Dictionary<HeroRarity, RarityUIInfo>(uiInfoByRarity);
        uiInfoByClassDict = new UI_Dictionary<HeroClass, ClassUIInfo>(uiInfoByClass);
    }

    public RarityUIInfo GetUIInfo(HeroRarity rarity)
    {
        return uiInfoByRarityDict.GetValue(rarity);
    }

    public ClassUIInfo GetUIInfo(HeroClass heroClass)
    {
        return uiInfoByClassDict.GetValue(heroClass);
    }

    [Serializable]
    public class UI_Dictionary<KeyType, ValueType>
    {
        public UI_Dictionary(UI_DictionaryItem[] items)
        {
            dictionary = new Dictionary<KeyType, ValueType>();
            foreach (UI_DictionaryItem item in items)
            {
                dictionary.Add((KeyType)item.GetKey(), (ValueType)item.GetValue());
            }
        }

        Dictionary<KeyType, ValueType> dictionary;

        public ValueType GetValue(KeyType key)
        {
            if (!dictionary.ContainsKey(key)) return default;
            return dictionary[key];
        }
    }

    public interface UI_DictionaryItem
    {
        public object GetKey();
        public object GetValue();
    }

    public interface IUIDictionary
    {
        public Type GetKeyType();
        public Type GetValueType();

    }

    [Serializable]
    public class UIInfoByRarity : UI_DictionaryItem
    {
        public HeroRarity heroRarity;
        public RarityUIInfo uiInfo;

        public object GetKey()
        {
            return heroRarity;
        }

        public object GetValue()
        {
            return uiInfo;
        }
    }

    [Serializable]
    public class UIInfoByClass : UI_DictionaryItem
    {
        public HeroClass heroClass;
        public ClassUIInfo uiInfo;

        public object GetKey()
        {
            return heroClass;
        }

        public object GetValue()
        {
            return uiInfo;
        }
    }

    [Serializable]
    public class RarityUIInfo
    {
        public Color gradientColorTop;
        public Color gradientColorBottom;
        public Sprite cardFrame;
    }

    [Serializable]
    public class ClassUIInfo
    {
        public Sprite symbol;
    }

}
