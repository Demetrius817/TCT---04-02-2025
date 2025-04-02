using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _monsterDistanceText;
    public void UpdateAmmo(int count) {

        _ammoText.text = "" + count.ToString("00");

    }
    public void UpdateMonsterDistance(int count) {

        _monsterDistanceText.text = "" + count;


    }
}
