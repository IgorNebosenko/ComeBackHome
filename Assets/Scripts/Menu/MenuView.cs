using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public void ShowCloseMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }
}
