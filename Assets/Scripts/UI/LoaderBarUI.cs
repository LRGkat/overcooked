using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoaderBarUI : MonoBehaviour
{
    [SerializeField] private LoaderCallback loaderCallback;
    [SerializeField] private Image loaderBar;
    [SerializeField] private TextMeshProUGUI pressKeyTipUI;
    [SerializeField] private TextMeshProUGUI text;
    private void Start()
    {
        pressKeyTipUI.gameObject.SetActive(false);
        loaderCallback.OnProgressChanged += LoaderCallbackOnonProgressChanged;
        loaderCallback.OnPressKeyTipUIShown += LoaderCallbackOnOnPressKeyTipUIShown;
    }

    private void LoaderCallbackOnOnPressKeyTipUIShown(object sender, EventArgs e)
    {
        pressKeyTipUI.gameObject.SetActive(true);
    }

    private void LoaderCallbackOnonProgressChanged(object sender, LoaderCallback.OnProgressChangedEventArgs e)
    {
        loaderBar.fillAmount = e.progressPercentage;
        text.text = (e.progressPercentage * 100).ToString() + "%";
    }
}
