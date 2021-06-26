using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tower_Defense.Towers
{
    public class PlayerTowerLevel : MonoBehaviour
    {
        private Camera _cam;
        [SerializeField] private GameObject towerGO;
        [SerializeField] Towers _tower;

        private Dictionary<Vector3, bool> _tileSelected = new Dictionary<Vector3, bool>();

        public Grid grid;
        private int priceIncreaser;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void OnMouseDown()
        {
            if (_tower != null)
            {
                CreateTower(ClickPosition());
            }
        }

        private Vector3 ClickPosition()
        {
            var worldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
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
            var counter = FindObjectOfType<TD_CoinCounter>();
            
            if (_tileSelected.ContainsKey(gridPosition))
            {
                var value = false;
                _tileSelected.TryGetValue(gridPosition, out value);
                if (value == false)
                {
                    if (counter.coins >= _tower.priceRunTime)
                    {
                        counter.coins -= _tower.priceRunTime;
                        _tower.priceRunTime += 10 + priceIncreaser;
                        priceIncreaser += 5;
                        var newTower = Instantiate(towerGO, gridPosition, Quaternion.identity);
                        _tileSelected[gridPosition] = true;
                        newTower.GetComponent<TowerDeploy>().tower = _tower;
                    }
                }
            }
        }

        public void SelectedTower(Towers towerSelected)
        {
            _tower = towerSelected;
        }
    }
}