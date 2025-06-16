using System.Collections.Generic;
using UnityEngine;

public class AdjustInContainter : MonoBehaviour
{
    [SerializeField] RectTransform container;
    List<RectTransform> items = new List<RectTransform>();
}
