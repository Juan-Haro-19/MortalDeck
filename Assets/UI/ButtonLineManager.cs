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
    private Dictionary<RectTransform, List<LineRenderer>> linesByButton = new();

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
    bool isPlayerCard = clickedButton.CompareTag("PlayerCard");
    if (!isDrawing)
    {
        if (!isPlayerCard)
        {
            return;
        }
        RemoveConnections(rect);
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
        bool destIsPlayerCard = isPlayerCard;
        bool originIsPlayerCard = currentStart.GetComponent<Button>().CompareTag("PlayerCard");
        if (!destIsPlayerCard || (originIsPlayerCard && destIsPlayerCard))
        {
            if (destIsPlayerCard)
            {
                RemoveConnections(rect);
            }
            Vector3 startPos = GetWorldPosition(currentStart);
            Vector3 endPos = GetWorldPosition(rect);

            currentLine.SetPosition(0, startPos);
            currentLine.SetPosition(1, endPos);
            linesByButton[currentStart] = new List<LineRenderer> { currentLine };
            if (destIsPlayerCard)
            {
                PlayerCardInfo info = rect.GetComponent<PlayerCardInfo>();
                if (info != null)
                {
                    info.childCards.Add(currentStart.gameObject);
                }
            }
            currentStart = null;
            currentLine = null;
            isDrawing = false;
        }
        else
        {
            Debug.Log("Conexión no permitida.");
            CancelCurrentLine();
        }
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
        foreach (var lineList in linesByButton.Values)
        {
            foreach (var line in lineList)
            {
                if (line != null)
                    Destroy(line.gameObject);
            }
        }
        linesByButton.Clear();
    }

    Vector3 GetWorldPosition(RectTransform rectTransform)
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rectTransform.position);
        return mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f));
    }
    void RemoveConnections(RectTransform rect)
    {
        if (linesByButton.ContainsKey(rect))
        {
            foreach (var line in linesByButton[rect])
            {
                if (line != null)
                    Destroy(line.gameObject);
            }
            linesByButton.Remove(rect);
        }
    }
}
