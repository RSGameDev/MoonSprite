using Tower_Defense.Towers;
using UnityEngine;

namespace Tower_Defense.UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private PlayerTowerLevel[] _playerTowerLevel;
        [SerializeField] private Towers.Towers tower;

        private void Awake()
        {
            _playerTowerLevel = FindObjectsOfType<PlayerTowerLevel>();
            tower.priceRunTime = tower.price;
        }

        private void OnMouseDown()
        {
            var buttons = FindObjectsOfType<TowerButton>();
            foreach (TowerButton button in buttons)
            {
                button.GetComponent<SpriteRenderer>().color = Color.white;
            }
            GetComponent<SpriteRenderer>().color = Color.yellow;

            // assigns tower to all playertowerlevel scripts in the scene.
            for (int i = 0; i < _playerTowerLevel.Length; i++)
            {
                _playerTowerLevel[i].SelectedTower(tower);
                _playerTowerLevel[i].AbleToBuildTileColour();
            }
        }
    }
}
