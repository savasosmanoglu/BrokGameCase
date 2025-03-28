using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Transform characterContainer;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float moveDistance = 600f;

    public static int SelectedCharacterIndex { get; private set; }
    private Tween _nextTween;
    private Tween _previousTween;

    public static Action<int> OnUpdateSelectedCharacterIndex;
    private DataManager _dataManager;

    private const string SCENE_GAME = "Scene_Game";

    [Inject]
    public void Construct(DataManager dataManager)
    {
        _dataManager = dataManager;
    }


    void OnEnable()
    {
        previousButton.onClick.AddListener(PreviousCharacter);
        nextButton.onClick.AddListener(NextCharacter);
        selectButton.onClick.AddListener(SelectCharacter);
        _dataManager.OnLoadData += InitPosition;
        _dataManager.OnSaveData += InitPosition;
    }

    void OnDisable()
    {
        previousButton.onClick.RemoveListener(PreviousCharacter);
        nextButton.onClick.RemoveListener(NextCharacter);
        selectButton.onClick.RemoveListener(SelectCharacter);
        _dataManager.OnLoadData -= InitPosition;
        _dataManager.OnSaveData -= InitPosition;
    }

    void Start()
    {
        InitPosition();
    }

    private void InitPosition()
    {
        characterContainer.localPosition = new Vector3(-SelectedCharacterIndex * moveDistance, characterContainer.localPosition.y, characterContainer.localPosition.z);
        OnUpdateSelectedCharacterIndex?.Invoke(SelectedCharacterIndex);
    }

    private void SelectCharacter()
    {
        SceneManager.LoadScene(SCENE_GAME);
    }

    private void NextCharacter()
    {
        if (SelectedCharacterIndex >= characters.Length - 1) return;
        SelectedCharacterIndex++;
        OnUpdateSelectedCharacterIndex?.Invoke(SelectedCharacterIndex);
        _nextTween.Kill();
        _previousTween.Kill();
        _nextTween = characterContainer.DOLocalMoveX(-SelectedCharacterIndex * moveDistance, moveSpeed);
    }

    private void PreviousCharacter()
    {
        if (SelectedCharacterIndex <= 0) return;
        SelectedCharacterIndex--;
        OnUpdateSelectedCharacterIndex?.Invoke(SelectedCharacterIndex);
        _nextTween.Kill();
        _previousTween.Kill();
        _previousTween = characterContainer.DOLocalMoveX(-SelectedCharacterIndex * moveDistance, moveSpeed);
    }
}
