using CosmoShip.Scripts.UI.Core.View;
using CosmoShip.Scripts.UI.Models.FinishGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoShip.Scripts.UI.Views.FinishGame
{
    public class FinishGameScreen : SimpleScreen<FinishGameModel>
    {
        [SerializeField] private Button _buttonMainMenu;
        [SerializeField] private TMP_Text _textCurrentScore;
        [SerializeField] private TMP_Text _textMaxScore;
        
        private FinishGameModel _model;

        public override void Bind(FinishGameModel model)
        {
            _model = model;
            model.OnGameOver += Show;
        }

        public override void Show()
        {
            Init();
            base.Show();
        }

        public override void Hide()
        {
            Dispose();
            base.Hide();
        }

        private void Init()
        {
            _textCurrentScore.text = $"Current score: {_model.CurrentScorePlayer}";
            _textMaxScore.text = $"Max score: {_model.LastResultScore}";
            _buttonMainMenu.onClick.AddListener(_model.LoadMainMenuScene);
        }

        public void Dispose()
        {
            _buttonMainMenu.onClick.RemoveListener(_model.LoadMainMenuScene);
        }
    }
}