using System.Collections.Generic;
using Project2.General;
using UnityEngine;

namespace GameData
{
    public static class SaveData
    {

        #region Properties
        public static int Level{
            get => PlayerPrefs.GetInt("level", 0);
            set => PlayerPrefs.SetInt("level", value);
        }
        public static float MoneyValue
        {
            get => PlayerPrefs.GetFloat("money", 0);
            set => PlayerPrefs.SetFloat("money", value);
        }

        #endregion
    }

}