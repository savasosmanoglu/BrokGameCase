using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeSkill : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI walkSpeedText;
    [SerializeField] private TextMeshProUGUI runSpeedText;
    [SerializeField] private TextMeshProUGUI jumpForceText;
    [SerializeField] private Button walkUpgradeButton;
    [SerializeField] private Button sprintUpgradeButton;
    [SerializeField] private Button jumpUpgradeButton;
    [SerializeField] private Button resetButton;
    private DataManager _dataManager;
    private CharacterData[] characterData;
    private int _selectedCharacterIndex = 0;

    [Inject]
    public void Construct(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    void OnEnable()
    {
        walkUpgradeButton.onClick.AddListener(SetWalkUpgrade);
        sprintUpgradeButton.onClick.AddListener(SetSprintUpgrade);
        jumpUpgradeButton.onClick.AddListener(SetJumpUpgrade);
        resetButton.onClick.AddListener(ResetData);
        CharacterSelector.OnUpdateSelectedCharacterIndex += UpdateSelectedCharacterIndex;
        _dataManager.OnSaveData += Initialize;
        _dataManager.OnLoadData += Initialize;
    }

    void OnDisable()
    {
        walkUpgradeButton.onClick.RemoveListener(SetWalkUpgrade);
        sprintUpgradeButton.onClick.RemoveListener(SetSprintUpgrade);
        jumpUpgradeButton.onClick.RemoveListener(SetJumpUpgrade);
        CharacterSelector.OnUpdateSelectedCharacterIndex -= UpdateSelectedCharacterIndex;
        _dataManager.OnSaveData -= Initialize;
        _dataManager.OnLoadData -= Initialize;
    }

    private void Initialize()
    {
        if (_dataManager == null)
        {
            return;
        }
        characterData = _dataManager.GetRuntimeCharacterDatas();
        if (characterData.Length <= 0)
        {
            return;
        }

        SetTexts();
    }

    private void UpdateSelectedCharacterIndex(int index)
    {
        _selectedCharacterIndex = index;
        SetTexts();
    }

    private void SetWalkUpgrade()
    {
        var data = characterData[_selectedCharacterIndex];
        data.SetWalkSpeed(data.walkSpeed + 1f);
        _dataManager.SaveData(data);
        walkSpeedText.text = data.walkSpeed.ToString();
    }

    private void SetSprintUpgrade()
    {
        var data = characterData[_selectedCharacterIndex];
        data.SetSprintSpeed(data.sprintSpeed + 1f);
        _dataManager.SaveData(data);
        runSpeedText.text = data.sprintSpeed.ToString();
    }

    private void SetJumpUpgrade()
    {
        var data = characterData[_selectedCharacterIndex];
        data.SetJumpForce(data.jumpForce + 1f);
        _dataManager.SaveData(data);
        jumpForceText.text = data.jumpForce.ToString();
    }
    private void SetTexts()
    {
        if (characterData is null || characterData.Length == 0)
        {
            characterData = _dataManager.GetRuntimeCharacterDatas();
        }
        var data = characterData[_selectedCharacterIndex];
        walkSpeedText.text = data.walkSpeed.ToString();
        runSpeedText.text = data.sprintSpeed.ToString();
        jumpForceText.text = data.jumpForce.ToString();
    }

    private void ResetData()
    {
        if (characterData is null || characterData.Length == 0)
        {
            characterData = _dataManager.GetRuntimeCharacterDatas();
        }
        var data = characterData[_selectedCharacterIndex];
        data.ResetData(_dataManager.GetDefaultCharacterData(_selectedCharacterIndex));
        _dataManager.SaveData(data);
        SetTexts();
    }
}
