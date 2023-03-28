using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressPercentage;
    }

    public event EventHandler OnPressKeyTipUIShown;
    
    private bool isFirstUpdate = false;
    private AsyncOperation operation;
    private float progress;
    private bool isShownPressKeyTipUI;
    private void Update()
    {
        if (!isFirstUpdate)
        {
            isFirstUpdate = true;
            operation = Loader.LoadCallback();
            operation.allowSceneActivation = false;
        }

        if (isFirstUpdate)
        {
            progress = operation.progress;
            if (progress < 0.9f)
            {
                OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
                {
                    progressPercentage = progress
                });
            }
            
            else if (progress >= 0.9f)
            {
                OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
                {
                    progressPercentage = 1.0f
                });
                if (!isShownPressKeyTipUI)
                {
                    OnPressKeyTipUIShown?.Invoke(this,EventArgs.Empty);
                }
                if (Input.anyKey)
                {
                    operation.allowSceneActivation = true;
                }
            }
        }
    }
}
