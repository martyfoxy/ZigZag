using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Менеджер UI
    /// </summary>
    public class UIManager : MonoBehaviour, IUIManager
    {
        private void Awake()
        {
            Debug.Log(">>> UI Manager Init");
        }
    }
}