using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string savePath;

    [SerializeField] private CharacterSO[] runtimeCharacterDatas;

    private List<CharacterData> saveData;

    public Action OnSaveData;
    public Action OnLoadData;

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "characterData.json");
        InitializeData();
    }

    private void InitializeData()
    {
        if (File.Exists(savePath))
        {
            LoadData();
        }
        else
        {
            InitData();
        }
    }

    private void InitData()
    {
        saveData = new List<CharacterData>();
        for (int i = 0; i < runtimeCharacterDatas.Length; i++)
        {
            saveData.Add(new CharacterData(runtimeCharacterDatas[i]));
        }
        string json = JsonUtility.ToJson(new SaveWrapper { characters = saveData }, true);
        File.WriteAllText(savePath, json);
        OnSaveData?.Invoke();
    }

    public void SaveData(CharacterData data)
    {
        if (saveData.Contains(data))
        {
            saveData[saveData.IndexOf(data)] = data;
        }
        else
        {
            saveData.Add(data);
        }
        string json = JsonUtility.ToJson(new SaveWrapper { characters = saveData }, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(savePath);
        SaveWrapper wrapper = JsonUtility.FromJson<SaveWrapper>(json);
        saveData = wrapper.characters;
        OnLoadData?.Invoke();
    }

    public CharacterSO GetDefaultCharacterData(int index) => runtimeCharacterDatas[index];


    public CharacterData[] GetRuntimeCharacterDatas()
    {
        return saveData.ToArray();
    }

    public CharacterData GetCharacterData(int index) => saveData[index];

    [Serializable]
    private class SaveWrapper
    {
        public List<CharacterData> characters;
    }
}

[Serializable]
public class CharacterData
{
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float HorizontalLookSensitivity;
    public float VerticalLookSensitivity;
    public Vector2 VerticalLookLimits;

    public CharacterData(CharacterSO data)
    {
        walkSpeed = data.WalkSpeed;
        sprintSpeed = data.SprintSpeed;
        jumpForce = data.JumpForce;
        HorizontalLookSensitivity = data.HorizontalLookSensitivity;
        VerticalLookSensitivity = data.VerticalLookSensitivity;
        VerticalLookLimits = data.VerticalLookLimits;
    }

    public void SetWalkSpeed(float value) => walkSpeed = value;
    public void SetSprintSpeed(float value) => sprintSpeed = value;
    public void SetJumpForce(float value) => jumpForce = value;

    public void ResetData(CharacterSO defaultData)
    {
        walkSpeed = defaultData.WalkSpeed;
        sprintSpeed = defaultData.SprintSpeed;
        jumpForce = defaultData.JumpForce;
    }
}
