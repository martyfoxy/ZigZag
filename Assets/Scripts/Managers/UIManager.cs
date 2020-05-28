using Assets.Scripts.Interfaces;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Менеджер UI
    /// </summary>
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField]
        private TextMeshProUGUI PressButtonTitle;

        [SerializeField]
        private TextMeshProUGUI YouLoseText;

        private void Awake()
        {
            Debug.Log(">>> UI Manager Init");
        }

        public void OnGameStateChanged(GameStateChangedSignal signal)
        {
            Debug.Log("OnGameStateChanged");
            switch(signal.NewState)
            {
                case GameStateEnum.Startup:
                    {
                        PressButtonTitle.gameObject.SetActive(true);
                        YouLoseText.gameObject.SetActive(false);
                        break;
                    }
                case GameStateEnum.Playing:
                    {
                        PressButtonTitle.gameObject.SetActive(false);
                        YouLoseText.gameObject.SetActive(false);
                        break;
                    }
                case GameStateEnum.GameOver:
                    {
                        PressButtonTitle.gameObject.SetActive(true);
                        YouLoseText.gameObject.SetActive(true);
                        break;
                    }
            }
        }
    }
}