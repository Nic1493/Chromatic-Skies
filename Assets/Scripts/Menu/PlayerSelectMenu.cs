using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectMenu : Menu
{
    EventSystem currentEventSystem;
    StandaloneInputModule inputModule;

    [SerializeField] ShipObject[] players;
    [SerializeField] Image[] statBarImages;
    float[,] fillAmounts;

    IEnumerator fillAnimation, colourAnimation;
    readonly AnimationCurve interpolationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    protected override void Awake()
    {
        base.Awake();

        currentEventSystem = EventSystem.current;
        inputModule = FindObjectOfType<StandaloneInputModule>();

        SetFillAmounts();
        AnimateStatBars(0);
    }

    void SetFillAmounts()
    {
        float[] maxStatValues = new float[]
        {
            players.Select(i => i.Health.OriginalValue).Max(),
            players.Select(i => i.Power.OriginalValue).Max(),
            players.Select(i => i.Defense.OriginalValue).Max(),
            players.Select(i => i.ShootingSpeed.OriginalValue).Max()
        };

        fillAmounts = new float[statBarImages.Length, players.Length];
        for (int i = 0; i < fillAmounts.GetLength(0); i++)
        {
            fillAmounts[0, i] = ((players[i].Health.OriginalValue / maxStatValues[0]));
            fillAmounts[1, i] = ((players[i].Power.OriginalValue / maxStatValues[1]));
            fillAmounts[2, i] = ((players[i].Defense.OriginalValue / maxStatValues[2]));
            fillAmounts[3, i] = ((players[i].ShootingSpeed.OriginalValue / maxStatValues[3]));
        }

        for (int i = 0; i < fillAmounts.GetLength(0); i++)
        {
            for (int j = 0; j < fillAmounts.GetLength(1); j++)
            {
                fillAmounts[i, j] = (fillAmounts[i, j] - 0.5f) * 2;
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown(inputModule.horizontalAxis) && fillAnimation == null)
        {
            int currentSelectedIndex = currentEventSystem.currentSelectedGameObject.transform.GetSiblingIndex();
            AnimateStatBars(currentSelectedIndex);
        }
    }

    void AnimateStatBars(int selectedPlayerIndex)
    {
        for (int i = 0; i < statBarImages.Length; i++)
        {
            fillAnimation = LerpFillAmount(i, fillAmounts[i, selectedPlayerIndex]);
            StartCoroutine(fillAnimation);

            colourAnimation = LerpColour(i, players[selectedPlayerIndex].UIColour);
            StartCoroutine(colourAnimation);
        }
    }

    IEnumerator LerpFillAmount(int barIndex, float endFill)
    {
        float startFill = statBarImages[barIndex].fillAmount;
        float currentLerpTime = 0f, totalLerpTime = 0.2f;

        while (statBarImages[barIndex].fillAmount != endFill)
        {
            float animationProgress = interpolationCurve.Evaluate(currentLerpTime / totalLerpTime);
            statBarImages[barIndex].fillAmount = Mathf.Lerp(startFill, endFill, animationProgress);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        fillAnimation = null;
    }

    IEnumerator LerpColour(int barIndex, Color endColour)
    {
        Color startColour = statBarImages[barIndex].color;
        float currentLerpTime = 0f, totalLerpTime = 0.1f;

        while (statBarImages[barIndex].color != endColour)
        {
            float animationProgress = interpolationCurve.Evaluate(currentLerpTime / totalLerpTime);
            statBarImages[barIndex].color = Color.Lerp(startColour, endColour, animationProgress);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        colourAnimation = null;
    }
}