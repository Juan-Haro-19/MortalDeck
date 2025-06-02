using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonLineManager : MonoBehaviour
{
    public Canvas canvas;               
    public Camera mainCamera;          
    public GameObject linePrefab;      
    private RectTransform currentStart;                   
    private bool isDrawing = false;                       
    private LineRenderer currentLine;                      
    private Dictionary<RectTransform, LineRenderer> linesByButton = new();  

    void Start()
    {
        Button[] allButtons = canvas.GetComponentsInChildren<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => OnButtonClick(btn));
        }
    }

    void Update()
    {
        if (isDrawing && currentStart != null && currentLine != null)
        {
            Vector3 startPos = GetWorldPosition(currentStart);
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
            currentLine.SetPosition(0, startPos);
            currentLine.SetPosition(1, mousePos);
        }
        if (isDrawing && Input.GetKeyDown(KeyCode.Escape))
        {
            CancelCurrentLine();
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        RectTransform rect = clickedButton.GetComponent<RectTransform>();
        if (!isDrawing)
        {
            if (!clickedButton.CompareTag("PlayerCard"))
            {
                return;
            }
            if (linesByButton.ContainsKey(rect))
            {
                Destroy(linesByButton[rect].gameObject);
                linesByButton.Remove(rect);
            }
            GameObject newLineObj = Instantiate(linePrefab);
            LineRenderer newLine = newLineObj.GetComponent<LineRenderer>();
            newLine.positionCount = 2;
            newLine.enabled = true;
            currentStart = rect;
            currentLine = newLine;
            isDrawing = true;
        }
        else
        {
            if (clickedButton.CompareTag("PlayerCard"))
            {
                CancelCurrentLine();
                return;
            }
            Vector3 startPos = GetWorldPosition(currentStart);
            Vector3 endPos = GetWorldPosition(rect);
            currentLine.SetPosition(0, startPos);
            currentLine.SetPosition(1, endPos);
            linesByButton[currentStart] = currentLine;
            currentStart = null;
            currentLine = null;
            isDrawing = false;
        }
    }

    public void CancelCurrentLine()
    {
        if (currentLine != null)
        {
            Destroy(currentLine.gameObject);
            currentLine = null;
        }
        currentStart = null;
        isDrawing = false;
    }

    public void ClearAllConnections()
    {
        foreach (var line in linesByButton.Values)
        {
            if (line != null)
                Destroy(line.gameObject);
        }
        linesByButton.Clear();
    }
    Vector3 GetWorldPosition(RectTransform rectTransform)
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rectTransform.position);
        return mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f));
    }
}