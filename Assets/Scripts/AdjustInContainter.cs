using System.Collections.Generic;
using UnityEngine;

public class AdjustInContainter : MonoBehaviour
{
    [SerializeField] RectTransform container;
    public RectTransform Container => container;
    List<RectTransform> items = new List<RectTransform>();
    public int ItemsInContainer => items.Count;

    public void AddItem(RectTransform _item) 
    {
        items.Add(_item);
        _item.SetParent(container);
        
        float xOffset = _item.sizeDelta.x/2;
        
        for(int i = 0; i <items.Count; i++)
        {
            xOffset *= -1;
            int offsetModifier = Mathf.CeilToInt((i+2)/2);
            items[i].localPosition = new Vector3((-xOffset * offsetModifier) - (_item.sizeDelta.x / 2), -container.sizeDelta.y *  0.5f, 0);
        }
    }
}
