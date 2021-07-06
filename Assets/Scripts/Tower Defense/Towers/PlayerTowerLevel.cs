using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        private bool canBuildOnTile;

        private Vector3Int _lastTilePosition;
        [SerializeField] private Color tileOriginalColour;

        private void Start()
        {
            cam = Camera.main;
            _counter = FindObjectOfType<TD_CoinCounter>();
        }

        private void OnMouseDown()
        {
            if (tower != null)
            {
                CreateTower(ClickPosition());
                AbleToDeployAgain();
            }
        }

        private void OnMouseOver()
        {
            // Last tile position goes back to normal colour
            if (_lastTilePosition != null)
            {
                OriginalTileColour(tileOriginalColour, _lastTilePosition, GetComponent<Tilemap>());
            }

            var worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            var gridPosition = grid.WorldToCell(worldPosition);
            AbleToBuildTileColour();
            if (canBuildOnTile)
            {
                TileColour(Color.green, gridPosition, GetComponent<Tilemap>());

                if (gameObject.layer == 9)
                {
                    TileColour(Color.red, gridPosition, GetComponent<Tilemap>());
                }
            }
            else
            {
                OriginalTileColour(tileOriginalColour, _lastTilePosition, GetComponent<Tilemap>());
            }

            _lastTilePosition = gridPosition;
        }

        private void OnMouseExit()
        {
            OriginalTileColour(tileOriginalColour, _lastTilePosition, GetComponent<Tilemap>());
        }

        // When the player can afford a tower, this outputs true. To allow for tiles to change colour.
        public void AbleToBuildTileColour()
        {
            if (tower == null)
            {
                return;
            }
            canBuildOnTile = _counter.coins >= tower.priceRunTime;
        }

        void OriginalTileColour(Color colour, Vector3Int position, Tilemap tilemap)
        {
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, tileOriginalColour);
        }

        void TileColour(Color colour, Vector3Int position, Tilemap tilemap)
        {
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, colour);
        }

        private Vector3 ClickPosition()
        {
            var worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            print(worldPosition);
            var gridPosition = grid.WorldToCell(worldPosition);
            print(gridPosition);
            var snappedPosition = new Vector3(gridPosition.x + 0.5f, gridPosition.y + 0.5f, gridPosition.z);
            if (!_tileSelected.ContainsKey(snappedPosition))
            {
                _tileSelected.Add(snappedPosition, false);
            }

            return snappedPosition;
        }

        private void CreateTower(Vector3 gridPosition)
        {
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