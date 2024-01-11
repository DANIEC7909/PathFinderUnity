using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class UIHeroPanel : MonoBehaviour
{
    [SerializeField] List<UIHeroButton> HeroesButtons;
    [SerializeField] RectTransform ButtonsContainer;
    private AsyncOperationHandle<GameObject> ButtonAssetHandle;
    [SerializeField] GameObject ButtonPrefab;
   public EventSystem EventSystem;

    [SerializeField] Button SaveButton;
    [SerializeField] Button LoadButton;
    private void OnEnable()
    {
        ButtonAssetHandle = Addressables.LoadAssetAsync<GameObject>("HeroSelectionButton");
        ButtonAssetHandle.WaitForCompletion();
        ButtonPrefab = ButtonAssetHandle.Result;
        GameEvents.Instance.OnAllHeroesSpawned += RegenerateButtons;
    }
    private void Start()
    {
        GameController.Instance.UIHeroPanel = this;
        SaveButton.onClick.AddListener(Save);
        LoadButton.onClick.AddListener(Load);
    }
    public void Save() => SaveManager.Instance.Save();
    public void Load() => SaveManager.Instance.Load();

   

    public void RegenerateButtons()
    {
       
        foreach (Hero hero in GameController.Instance.AllSpawnedHeros)
        {
            UIHeroButton uIHB = Instantiate(ButtonPrefab, ButtonsContainer).GetComponent<UIHeroButton>();
            uIHB.Build(hero);
            HeroesButtons.Add(uIHB);
        }
    }
    private void OnDestroy()
    {
        Addressables.Release(ButtonAssetHandle);
        GameEvents.Instance.OnAllHeroesSpawned -= RegenerateButtons;
        SaveButton.onClick.RemoveListener(SaveManager.Instance.Save);
        LoadButton.onClick.RemoveListener(SaveManager.Instance.Load);
    }
}
