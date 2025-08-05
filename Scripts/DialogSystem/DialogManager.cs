using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogText;
    public Image portraitImage;

    private DialogLine[] currentLines;
    private int currentIndex;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogPanel.SetActive(false);
    }

    private void Update()
    {
        if (dialogPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialog(DialogLine[] lines)
    {
        currentLines = lines;
        currentIndex = 0;
        dialogPanel.SetActive(true);
        ShowLine(currentLines[currentIndex]);
    }

    private void NextLine()
    {
        currentIndex++;
        if (currentIndex < currentLines.Length)
        {
            ShowLine(currentLines[currentIndex]);
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }

    private void ShowLine(DialogLine line)
    {
        speakerNameText.text = line.speakerName;
        // Проверяем, нужно ли применять DovaFont
        if (line.useDovahFont) // или любая другая проверка
        {
            string translated = DovahTransliterator.Transliterate(line.text);
            dialogText.text = $"<font=\"DovaFont\">{translated}</font>";
        }
        else
        {
            dialogText.text = line.text;
        }

        if (line.portrait != null)
        {
            portraitImage.sprite = line.portrait;
            portraitImage.gameObject.SetActive(true);
        }
        else
        {
            portraitImage.gameObject.SetActive(false);
        }
    }
}