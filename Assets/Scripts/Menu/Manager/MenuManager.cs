using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    [SerializeField] private Menu[] menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        foreach (var t in menus)
        {
            if (t.menuName == menuName)
            {
                t.Open();
            }
            else if (t.open)
            {
                t.Close();
            }
        }
    }

    public void CloseMenu(string menuName)
    {
        foreach (var t in menus)
        {
            if (t.menuName == menuName)
            {
                t.Close();
            }
        }
        
    }

    public void CloseAllOpenMenus()
    {
        foreach (var t in menus)
        {
            if (t.open)
            {
                t.Close();
            }
        }
    }

    public bool IsMenuOpen(string menuName)
    {
        return menus.Any(t => t.menuName == menuName && t.open);
    }
}
