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
    bool isEnemyCard = clickedButton.CompareTag("EnemyCard");

    // === INICIO DE CONEXIÓN ===
    if (!isDrawing)
    {
        if (!isPlayerCard)
        {
            Debug.Log("Solo los botones con el tag 'PlayerCard' pueden iniciar una conexión.");
            return;
        }

        // Si ya tiene conexión previa como origen, eliminarla
        if (linesByButton.ContainsKey(rect))
        {
            RemoveConnections(rect);
        }

        // Crear línea nueva
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
        // === DESTINO DE CONEXIÓN ===
        bool destIsPlayerCard = isPlayerCard;
        bool originIsPlayerCard = currentStart.GetComponent<Button>().CompareTag("PlayerCard");

        // Solo se permiten conexiones si:
        // - PlayerCard → EnemyCard
        // - PlayerCard → PlayerCard
        if (originIsPlayerCard && (destIsPlayerCard || isEnemyCard))
        {
            // Si el destino ya tiene línea, la eliminamos
            if (linesByButton.ContainsKey(rect))
            {
                RemoveConnections(rect);
            }

            // Crear la línea visual
            Vector3 startPos = GetWorldPosition(currentStart);
            Vector3 endPos = GetWorldPosition(rect);

            currentLine.SetPosition(0, startPos);
            currentLine.SetPosition(1, endPos);

            // Registrar línea en origen
            linesByButton[currentStart] = new List<LineRenderer> { currentLine };

            // Obtener info del botón origen
            PlayerCardInfo originInfo = currentStart.GetComponent<PlayerCardInfo>();
            if (originInfo != null)
            {
                if (destIsPlayerCard)
                {
                    // El destino guarda referencia al origen
                    PlayerCardInfo destInfo = rect.GetComponent<PlayerCardInfo>();
                    if (destInfo != null)
                    {
                        destInfo.childCards.Add(currentStart.gameObject);
                        currentStart.GetComponent<PlayerCardInfo>().isACardChild = true;
                    }
                }
                else if (isEnemyCard)
                {
                    // El origen guarda referencia al enemigo
                    originInfo.ClashCard = rect.gameObject;
                }
            }

            // Finalizar conexión
            currentStart = null;
            currentLine = null;
            isDrawing = false;
        }
        else
        {
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
        Button[] allButtons = canvas.GetComponentsInChildren<Button>();
        foreach (Button btn in allButtons)
        {
            if (btn.CompareTag("PlayerCard"))
            {
                btn.GetComponent<PlayerCardInfo>().isACardChild = false;
                btn.GetComponent<PlayerCardInfo>().childCards.Clear();
                btn.GetComponent<PlayerCardInfo>().ClashCard = null;
                
            }
        }
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
