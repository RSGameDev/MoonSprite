using System;
using UnityEngine;

namespace Tower_Defense.Towers
{
    public class PlayerTowerLevel : MonoBehaviour
    {
        private Camera cam;
        [SerializeField] private GameObject towerGO;
        [SerializeField] Towers _tower;

        public bool onSelectScreen;
        
        void Start()
        {
            cam = Camera.main;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !onSelectScreen)
            {
                CreateTower(ClickPosition());
            }
        }

        public Vector2 ClickPosition()
        {
            Debug.Log("ClickPosition");
            var worldPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y ,cam.nearClipPlane));
                var gridPosition = SnapToTile(worldPosition);
                //Debug.Log(worldPosition);
                return gridPosition;
        }
    
        public Vector2 SnapToTile(Vector2 worldPosition) // Me trying to work out how to snap it how i wanted.
        {
            Debug.Log("SnapToTile");
            //var newX = (float)Math.Round(worldPosition.x * 2) * 0.5f;
            //var newX = (float)Math.Round(worldPosition.x * 2, MidpointRounding.AwayFromZero) * 0.5f;
            var newX = (float)Math.Round(worldPosition.x * 2, MidpointRounding.AwayFromZero) / 2;
            var newY = (float)Math.Round(worldPosition.y * 2, MidpointRounding.AwayFromZero) / 2;
            //var newY = (float)Math.Round(worldPosition.y * 2, MidpointRounding.AwayFromZero) / 2;
            //float newY = Mathf.RoundToInt(worldPosition.y);
 
            return new Vector2(newX, newY);
        }

        public void CreateTower(Vector2 gridPosition)
        {
            Debug.Log("CreateTower");
            var newTower = Instantiate(towerGO, gridPosition, Quaternion.identity);
            newTower.GetComponent<TowerDeploy>().tower = _tower;
        }

        public void SelectedTower(Towers towerSelected)
        {
            _tower = towerSelected;
        }
    }
}
