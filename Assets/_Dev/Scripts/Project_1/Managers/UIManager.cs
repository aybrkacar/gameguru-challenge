using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Project1.General
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Button _rebuildButton;
        [SerializeField] private TMP_InputField _inputField;

        private int _matchCount = 0;
        #endregion

        #region Mono
        void Start()
        {   
            GameManager.OnMatched += UpdateMatchCount;
            _rebuildButton.onClick.AddListener(Rebuild);
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region Methods
        private void Rebuild()
        {
            int userInput;
            if (int.TryParse(_inputField.text, out userInput))
            {
                GameManager.Instance.GridGenerator.gridSize = userInput;
                GameManager.Instance.GridGenerator.GenerateGrid();
            }
            else
            {
                Debug.LogWarning("Girilen değer tam sayıya dönüştürülemedi!");
            }
        }

        public void IncreaseMatchCount()
        {
            _matchCount++;
        }

        public void UpdateMatchCount()
        {
            IncreaseMatchCount();
            _countText.text = _matchCount.ToString("F0");
        }
        #endregion

    }
}
