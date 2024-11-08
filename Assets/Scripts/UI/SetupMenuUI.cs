using System.Collections.Generic;
using UnityEngine;

public class SetupMenuUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> defaultActiveMenus;  // GameObjects to automatically activate at startup
    [SerializeField] private List<GameObject> defaultInactiveMenus; // GameObjects to automatically deactivate at startup

    // Initialization method: sets up menus with default states
    private void Start()
    {
        SetupMenus();
    }

    // Method to set up menus based on default lists
    private void SetupMenus()
    {
        // Activate all menus in the defaultActiveMenus list
        foreach (GameObject menu in defaultActiveMenus)
        {
            if (menu != null)
            {
                menu.SetActive(true);
            }
        }

        // Deactivate all menus in the defaultInactiveMenus list
        foreach (GameObject menu in defaultInactiveMenus)
        {
            if (menu != null)
            {
                menu.SetActive(false);
            }
        }
    }

    // Method to toggle a specific menu on or off by name
    public void SetMenuActive(string menuName, bool isActive)
    {
        GameObject menu = FindMenuByName(menuName);
        if (menu != null)
        {
            menu.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning($"Menu '{menuName}' not found!");
        }
    }

    // Helper method to search for a menu by name within the lists
    private GameObject FindMenuByName(string menuName)
    {
        // Search in the defaultActiveMenus list
        foreach (GameObject menu in defaultActiveMenus)
        {
            if (menu.name == menuName)
            {
                return menu;
            }
        }

        // Search in the defaultInactiveMenus list
        foreach (GameObject menu in defaultInactiveMenus)
        {
            if (menu.name == menuName)
            {
                return menu;
            }
        }

        // Return null if the menu is not found
        return null;
    }
}
