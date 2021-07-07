using System;
using System.Collections;
using System.Collections.Generic;
using Tower_Defense.Towers;
using UnityEngine;
using UnityEngine.UI;

public class TowerMouse : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] rangeOfTower;
    private SpriteRenderer currentTowerSelected;

    [SerializeField] private PlayerTowerLevel _playerTowerLevel;

    private void Update()
    {
        if (_playerTowerLevel.showRange)
        {
            switch (_playerTowerLevel.tower.name)
            {
                case "tower rapid fire":
                    rangeOfTower[0].gameObject.SetActive(true);
                    rangeOfTower[1].gameObject.SetActive(false);
                    rangeOfTower[2].gameObject.SetActive(false);
                    currentTowerSelected = rangeOfTower[0];
                    break;
                case "Kettle":
                    rangeOfTower[1].gameObject.SetActive(true);
                    rangeOfTower[0].gameObject.SetActive(false);
                    rangeOfTower[2].gameObject.SetActive(false);
                    currentTowerSelected = rangeOfTower[1];
                    break;
                case "OvenTower":
                    rangeOfTower[2].gameObject.SetActive(true);
                    rangeOfTower[0].gameObject.SetActive(false);
                    rangeOfTower[1].gameObject.SetActive(false);
                    currentTowerSelected = rangeOfTower[2];
                    break;
            }

            Vector3 mousePos = _playerTowerLevel.cam.ScreenToWorldPoint(Input.mousePosition);
            currentTowerSelected.transform.position = new Vector3(mousePos.x,mousePos.y,0f);
        }
        else
        {
            for (int i = 0; i < rangeOfTower.Length; i++)
            {
                rangeOfTower[i].gameObject.SetActive(false);
            }
        }
    }
}