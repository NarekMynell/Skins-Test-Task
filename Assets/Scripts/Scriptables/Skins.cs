using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Skins", menuName = "ScriptableObjects/Skins", order = 2)]
public class Skins : ScriptableObject, IEnumerable<SkinData>
{
    [SerializeField] private List<SkinData> skins;
    private HashSet<SkinData> unlockedSkins;
    private HashSet<SkinData> lockedSkins;
    [HideInInspector] public SkinData selectedSkin;
    public int Count { get { return skins.Count; } }
    public int LockedSkinsCount { get { return lockedSkins.Count; } }

    private void OnEnable()
    {
        // Check to prevent additional initialization when the scene changes
        // or the object reappears in the scene
        if (unlockedSkins == null)
        {
            unlockedSkins = new();
            lockedSkins = new(skins);
        }
    }

    public SkinData UnlockRandomSkin()
    {
        if (lockedSkins.Count == 0) return null;

        SkinData randomSkin = lockedSkins.GetRandomItem();
        randomSkin.Unlock();
        lockedSkins.Remove(randomSkin);
        unlockedSkins.Add(randomSkin);

        return randomSkin;
    }

    public IEnumerator<SkinData> GetEnumerator()
    {
        return skins.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Save(string skinsStateSavingPath, string selectedSkinSavingKey)
    {
        if (unlockedSkins == null || unlockedSkins.Count == 0) return;

        // Save skins state
        List<int> unlockedSkinIds = new(unlockedSkins.Count);

        foreach(SkinData skinData in unlockedSkins)
        {
            unlockedSkinIds.Add(skinData.Id);
        }
        string json = JsonConvert.SerializeObject(unlockedSkinIds);
        File.WriteAllText(skinsStateSavingPath, json);

        // Save selected skin id
        if (selectedSkin != null)
        {
            PlayerPrefs.SetInt(selectedSkinSavingKey, selectedSkin.Id);
        }
    }

    public void Load(string skinsStateSavingPath, string selectedSkinSavingKey)
    {
        // Load selected skin
        int selectedSkinId = PlayerPrefs.GetInt(selectedSkinSavingKey, -1);
        // Load skins state
        if (File.Exists(skinsStateSavingPath))
        {
            string json = File.ReadAllText(skinsStateSavingPath);
            List<int> unlockedSkinIdsList = JsonConvert.DeserializeObject<List<int>>(json);
            HashSet<int> unlockedSkinIdsSet = new(unlockedSkinIdsList);
            
            unlockedSkins = new();
            lockedSkins = new();

            foreach(SkinData skinData in skins)
            {
                if(unlockedSkinIdsSet.Contains(skinData.Id))
                {
                    skinData.Initialize(true);
                    unlockedSkins.Add(skinData);

                    if(skinData.Id == selectedSkinId)
                    {
                        selectedSkin = skinData;
                    }
                }
                else
                {
                    lockedSkins.Add(skinData);
                }
            }
        }
    }
}
