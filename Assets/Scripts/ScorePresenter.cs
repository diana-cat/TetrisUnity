using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void ScoreShow(int score)
    {
        text.text = "SCORE: " + score;
    }
}
