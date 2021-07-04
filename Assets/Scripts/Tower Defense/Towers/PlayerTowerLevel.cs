using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tower_Defense.Towers
{
    public class PlayerTowerLevel : MonoBehaviour
    {
        public Camera cam;
        [SerializeField] private GameObject towerGO;
        public Towers tower;

        private Dictionary<Vector3, bool> _tileSelected = new Dictionary<Vector3, bool>();

        public Grid grid;
        private int priceIncreaser;
        public bool showRange;
        private TD_CoinCounter _counter;
        private PlayerTowerLevel[] _playerTowerLevel;
            
        private void Start()
        {
            cam = Camera.main;
        }

        private void OnMouseDown()
        {
            if (tower != null)
            {
                CreateTower(ClickPosition());
                AbleToDeployAgain();
            }
        }

        private Vector3 ClickPosition()
        {
            var worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            var gridPosition = grid.WorldToCell(worldPosition);
            var snappedPosition = new Vector3(gridPosition.x + 0.5f, gridPosition.y + 0.5f, gridPosition.z);
            if (!_tileSelected.ContainsKey(snappedPosition))
            {
                _tileSelected.Add(snappedPosition, false);
            }
            return snappedPosition;
        }

        private void CreateTower(Vector3 gridPosition)
        {
            _counter = FindObjectOfType<TD_CoinCounter>();
            
            if (_tileSelected.ContainsKey(gridPosition))
            {
                var value = false;
                _tileSelected.TryGetValue(gridPosition, out value);
                if (value == false)
                {
                    if (_counter.coins >= tower.priceRunTime)
                    {
                        _counter.coins -= tower.priceRunTime;
                        tower.priceRunTime += 10 + priceIncreaser;
                        priceIncreaser += 5;
                        var newTower = Instantiate(towerGO, gridPosition, Quaternion.identity);
                        _tileSelected[gridPosition] = true;
                        newTower.GetComponent<TowerDeploy>().tower = tower;
                    }
                }
            }
        }

        private void AbleToDeployAgain()
        {
            _playerTowerLevel = FindObjectsOfType<PlayerTowerLevel>();
            if (_counter.coins < tower.priceRunTime)
            {
                print(showRange);
                for (int i = 0; i < _playerTowerLevel.Length; i++)
                {
                    _playerTowerLevel[i].showRange = false;
                }
                print(showRange);
            }
        }

        public void SelectedTower(Towers towerSelected)
        {
            tower = towerSelected;
            showRange = true;
        }
    }
}