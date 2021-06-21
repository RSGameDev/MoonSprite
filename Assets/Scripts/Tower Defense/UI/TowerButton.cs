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
        }

        private void OnMouseDown()
        {
            var buttons = FindObjectsOfType<TowerButton>();
            foreach (TowerButton button in buttons)
            {
                button.GetComponent<SpriteRenderer>().color = Color.white;
            }
            GetComponent<SpriteRenderer>().color = Color.yellow;

            for (int i = 0; i < _playerTowerLevel.Length; i++)
            {
                _playerTowerLevel[i].SelectedTower(tower);
            }
        }
    }
}
