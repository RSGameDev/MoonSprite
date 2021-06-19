using System;
using System.Collections;
using System.Collections.Generic;
using Tower_Defense.Towers;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private PlayerTowerLevel _playerTowerLevel;
    [SerializeField] private Towers tower;

    private void OnMouseDown()
    {
        _playerTowerLevel.onSelectScreen = true;
        var buttons = FindObjectsOfType<TowerButton>();
        foreach (TowerButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = Color.white;
        }
        GetComponent<SpriteRenderer>().color = Color.yellow;
        FindObjectOfType<PlayerTowerLevel>().SelectedTower(tower);
    }

    private void OnMouseUp()
    {
        _playerTowerLevel.onSelectScreen = false;
    }
}
