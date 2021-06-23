using System;
using UnityEngine;

namespace Tower_Defense.Towers
{
    public class PlayerTowerLevel : MonoBehaviour
    {
        private Camera _cam;
        [SerializeField] private GameObject towerGO;
        [SerializeField] Towers _tower;

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
            return snappedPosition;
        }

        private void CreateTower(Vector3 gridPosition)
        {
            var counter = FindObjectOfType<TD_CoinCounter>();
            if (counter.coins>=_tower.priceRunTime)
            {
                counter.coins -= _tower.priceRunTime;
                _tower.priceRunTime += 10 + priceIncreaser;
                priceIncreaser += 5;
                var newTower = Instantiate(towerGO, gridPosition, Quaternion.identity);
                newTower.GetComponent<TowerDeploy>().tower = _tower;
            }
            
        }

        public void SelectedTower(Towers towerSelected)
        {
            _tower = towerSelected;
        }
    }
}