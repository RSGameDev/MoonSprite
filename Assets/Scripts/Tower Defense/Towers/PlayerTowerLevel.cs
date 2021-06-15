using System;
using UnityEngine;

namespace Tower_Defense.Towers
{
    public class PlayerTowerLevel : MonoBehaviour
    {
        private Camera cam;
        public GameObject tower;
    
        void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {
            ClickPosition();
        }
    
        //public Vector2 GetSquareClicked() //Finds mouse click position
        public void ClickPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y ,cam.nearClipPlane));
                var gridPosition = SnapToTile(worldPosition);
                CreateTower(gridPosition);
        
                //Debug.Log(worldPosition);
            }
        
        }
    
        public Vector2 SnapToTile(Vector2 worldPosition) // Me trying to work out how to snap it how i wanted.
        {
            //var newX = (float)Math.Round(worldPosition.x * 2) * 0.5f;
            //var newX = (float)Math.Round(worldPosition.x * 2, MidpointRounding.AwayFromZero) * 0.5f;
            var newX = (float)Math.Round(worldPosition.x, MidpointRounding.AwayFromZero);
            var newY = (float)Math.Round(worldPosition.y, MidpointRounding.AwayFromZero);
            //var newY = (float)Math.Round(worldPosition.y * 2, MidpointRounding.AwayFromZero) / 2;
            //float newY = Mathf.RoundToInt(worldPosition.y);
 
            return new Vector2(newX, newY);
        }

        public void CreateTower(Vector2 gridPosition)
        {
            Instantiate(tower, gridPosition, Quaternion.identity);
        }
    }
}
