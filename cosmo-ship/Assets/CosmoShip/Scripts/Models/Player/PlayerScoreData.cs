using UnityEngine;

namespace CosmoShip.Scripts.Models.Player
{
    public class PlayerScoreData
    {
        private string _keyScore = "PlayerScore";
        public int GetMaxScore
        {
            get
            {
                if (PlayerPrefs.HasKey(_keyScore))
                {
                    return PlayerPrefs.GetInt(_keyScore);
                }
                return 0;
            }
            set
            {
                PlayerPrefs.SetInt(_keyScore, value);
            }
        }
    }
}
