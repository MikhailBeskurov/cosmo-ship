using CosmoShip.Scripts.UI.Core.View;
using CosmoShip.Scripts.UI.Models.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoShip.Scripts.UI.Views.MainMenu
{
    public class MainMenuScreen : SimpleScreen<MainMenuModel>
    {
        [SerializeField] private Button _buttonPlayGame;
        [SerializeField] private TMP_Text _textScoreMax;
        
        private MainMenuModel _model;

        public override void Bind(MainMenuModel model)
        {
            _model = model;
            _textScoreMax.text = $"Max score: {model.MaxScorePlayer}";
            _buttonPlayGame.onClick.AddListener(model.LoadBattleScene);
        }

        public override void Hide()
        {
            Dispose();
            base.Hide();
        }

        public void Dispose()
        {
            _buttonPlayGame.onClick.RemoveListener(_model.LoadBattleScene);
        }
    }
}