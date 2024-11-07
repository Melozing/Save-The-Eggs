using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [SerializeField] private Transform popupContainer;
    [SerializeField] private List<PopupPrefab> popupPrefabs;  // danh sách các prefab pop-up

    private Dictionary<PopupType, GameObject> popupDictionary = new Dictionary<PopupType, GameObject>();
    private Stack<GameObject> popupStack = new Stack<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePopupDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePopupDictionary()
    {
        foreach (var popupPrefab in popupPrefabs)
        {
            popupDictionary[popupPrefab.popupType] = popupPrefab.prefab;
        }
    }

    public void ShowPopup(PopupType popupType, string message = "", System.Action onClose = null)
    {
        if (popupDictionary.TryGetValue(popupType, out GameObject popupPrefab))
        {
            GameObject popupInstance = Instantiate(popupPrefab, popupContainer);
            Popup popupComponent = popupInstance.GetComponent<Popup>();

            popupComponent.Setup(message, onClose);
            popupStack.Push(popupInstance);
        }
        else
        {
            Debug.LogError("Popup type not found: " + popupType);
        }
    }

    public void CloseCurrentPopup()
    {
        if (popupStack.Count > 0)
        {
            GameObject popupToClose = popupStack.Pop();
            Destroy(popupToClose);
        }
    }

    public void CloseAllPopups()
    {
        while (popupStack.Count > 0)
        {
            Destroy(popupStack.Pop());
        }
    }
}
