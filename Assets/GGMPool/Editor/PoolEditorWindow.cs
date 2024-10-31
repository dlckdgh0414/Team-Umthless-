using GGMPool;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [SerializeField] private VisualTreeAsset _itemUXMLAsset = default;
    [SerializeField] private PoolManagerSO _poolManager = default;
    [SerializeField] private ToolInfoSO _toolInfo = default;
    private Button _createBtn;
    private ScrollView _itemView;

    private List<PoolItem> _itemList = new List<PoolItem>();
    private PoolItem _currentItem;

    private ItemInspector _inspector;

    [MenuItem("Tools/PoolEditorWindow")]
    public static void ShowWindow()
    {
        PoolEditorWindow wnd = GetWindow<PoolEditorWindow>();
        wnd.titleContent = new GUIContent("Poolmanager");
        wnd.minSize = new Vector2(700, 600);
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        VisualElement content = m_VisualTreeAsset.Instantiate();
        content.style.flexGrow = 1;
        root.Add(content);

        InitializeItems(content);
        GeneratePoolingItemUI();
    }

    private void GeneratePoolingItemUI()
    {
        _itemView.Clear();
        _itemList.Clear();
        _inspector.ClearInspector();

        if (_poolManager.poolingItemList.Count <= 0) return;
        foreach (PoolingItemSO item in _poolManager.poolingItemList)
        {
            var itemUIAsset = _itemUXMLAsset.Instantiate();
            PoolItem poolItem = new PoolItem(itemUIAsset, item);
            _itemView.Add(itemUIAsset);
            _itemList.Add(poolItem);

            poolItem.Name = item.name;
            poolItem.OnSelectEvent += HandleSelectItem;
            poolItem.OnDeleteEvent += HandleDeleteItem;
        }
    }

    private void HandleDeleteItem(PoolItem item)
    {
        _poolManager.poolingItemList.Remove(item.itemSO);
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item.itemSO.poolType));
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item.itemSO));
        EditorUtility.SetDirty(_poolManager);

        AssetDatabase.SaveAssets();

        if (_currentItem == item)
        {
            _currentItem = null;
            //나중에 인스펙터도 클리어한다.
        }

        GeneratePoolingItemUI();
    }

    private void HandleSelectItem(PoolItem item)
    {
        _itemList.ForEach(item => item.IsActive = false);
        item.IsActive = true;
        _currentItem = item;
        _inspector.UpdateInspector(_currentItem.itemSO);
    }

    private void InitializeItems(VisualElement content)
    {
        _createBtn = content.Q<Button>("BtnCreate");
        _itemView = content.Q<ScrollView>("ItemView");

        _itemView.Clear();

        _inspector = new ItemInspector(content, this);
        _inspector.NameChangeEvent += HandleAssetNameChange;
        _createBtn.clicked += HandleCreateItem;
    }

    private void HandleAssetNameChange(PoolingItemSO target, string newName)
    {
        string typePath = AssetDatabase.GetAssetPath(target.poolType);
        string itemPath = AssetDatabase.GetAssetPath(target);

        bool exists = _poolManager.poolingItemList.Any(item => item.poolType.name.Equals(newName));

        if (exists)
        {
            EditorUtility.DisplayDialog("Duplicated name!", $"Given asset name {newName} is already exist", "OK");
            return;
        }

        AssetDatabase.RenameAsset(typePath, $"{newName}_Type");
        AssetDatabase.RenameAsset(itemPath, $"{newName}_Item");
        target.poolType.typeName = newName;

        EditorUtility.SetDirty(target.poolType);
        AssetDatabase.SaveAssets();

        GeneratePoolingItemUI();
    }

    private void HandleCreateItem()
    {
        Guid typeGuid = Guid.NewGuid();
        PoolTypeSO typeSO = ScriptableObject.CreateInstance<PoolTypeSO>();
        typeSO.typeName = typeGuid.ToString();

        string typeFileName = $"{typeSO.typeName}.asset";
        string typeFilePath = $"{_toolInfo.poolingFolder}/{_toolInfo.typeFolder}";
        CreateFolderIfNotExist(typeFilePath);

        AssetDatabase.CreateAsset(typeSO, $"{typeFilePath}/{typeFileName}");

        PoolingItemSO itemSO = ScriptableObject.CreateInstance<PoolingItemSO>();
        itemSO.poolType = typeSO;

        string itemFileName = $"{typeSO.typeName}.asset";
        string itemFilePath = $"{_toolInfo.poolingFolder}/{_toolInfo.itemFolder}";
        CreateFolderIfNotExist(itemFilePath);

        AssetDatabase.CreateAsset(itemSO, $"{itemFilePath}/{itemFileName}");

        _poolManager.poolingItemList.Add(itemSO);

        EditorUtility.SetDirty(_poolManager);
        AssetDatabase.SaveAssets();

        GeneratePoolingItemUI();
    }

    private void CreateFolderIfNotExist(string path)
    {
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
    }

    private void OnDestroy()
    {
        _inspector.Dispose();
    }
}
