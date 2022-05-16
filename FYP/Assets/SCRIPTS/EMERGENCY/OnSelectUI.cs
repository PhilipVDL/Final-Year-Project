using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnSelectUI : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Button button;
    public bool controlsOptions;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    #region selectDeselect
    public void OnSelect(BaseEventData eventData)
    {
        SetNative();

        //Debug.Log(gameObject.name + " was selected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        SetNative();

        //Debug.Log(gameObject.name + " was deselected");
    }
    #endregion

    #region controlsOptions
    void SetNative()
    {
        if (controlsOptions)
        {
            button.image.SetNativeSize();
        }
    }
    #endregion
}