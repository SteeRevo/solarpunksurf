using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionIndicator : MonoBehaviour, IDeselectHandler, ISelectHandler
{

    [SerializeField]
    private Image selectedSprite;

    
  
    void Start()
    {
        selectedSprite.enabled = false;
    }

    public void OnSelect(BaseEventData data)
    {
        selectedSprite.enabled = true;
    }

    public void OnDeselect(BaseEventData data)
    {
        selectedSprite.enabled = false;
    }

    
}
