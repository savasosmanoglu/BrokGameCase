using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class CharacterLoader : MonoBehaviour
{
    public PlayerController PlayerController { get; private set; }
    private GameObject currentCharacter;
    private Camera mainCamera;
    private ICharacterFactory characterFactory;

    private CharacterData _currentCharacterData;
    private DataManager _dataManager;
    [Inject]
    public void Construct(Camera mainCamera, ICharacterFactory characterFactory, DataManager dataManager)
    {
        _dataManager = dataManager;
        this.mainCamera = mainCamera;
        this.characterFactory = characterFactory;
        LoadCharacter(CharacterSelector.SelectedCharacterIndex);
    }

    private async void LoadCharacter(int index)
    {
        currentCharacter = await characterFactory.CreateCharacter(index);
        PlayerController = currentCharacter.GetComponent<PlayerController>();
        _currentCharacterData = _dataManager.GetCharacterData(index);
        currentCharacter.GetComponent<CharacterMovement>().SetCharacterData(_currentCharacterData);
        EventManager.OnCharacterLoaded?.Invoke(_currentCharacterData);
        SetupCamera();
    }

    private void SetupCamera()
    {
        if (currentCharacter == null || mainCamera == null) return;

        mainCamera.transform.SetParent(currentCharacter.transform);

        mainCamera.transform.localPosition = new Vector3(0, 1.6f, 0.2f);
        mainCamera.transform.localRotation = Quaternion.identity;
    }

}