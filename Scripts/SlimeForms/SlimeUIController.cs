using UnityEngine;
using UnityEngine.UI;

public class SlimeUIController : MonoBehaviour
{
    [System.Serializable]
    public class FormSegment
    {
        public Image segment;
        public Color baseColor;
        [HideInInspector] public Color activeColor;
    }

    public FormSegment[] segments;

    void Start()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].activeColor = new Color(
                segments[i].baseColor.r,
                segments[i].baseColor.g,
                segments[i].baseColor.b,
                1f
            );

            if (i == 0)
            {
                // Дефолтная форма активна с самого начала
                segments[i].segment.color = segments[i].activeColor;
            }
            else
            {
                // Остальные формы заблокированы (блеклый/серый цвет + полупрозрачность)
                segments[i].segment.color = new Color(
                    segments[i].baseColor.r * 0.5f,
                    segments[i].baseColor.g * 0.5f,
                    segments[i].baseColor.b * 0.5f,
                    0.3f
                );
            }

            // fillAmount НЕ трогаем! он 0.25 у всех всегда!
        }
    }


    public void UpdateProgress(string formType, int collected, int required)
    {
        int index = GetFormIndex(formType);
        if (index == -1) return;

        if (formType == "default") return; // Дефолтная форма не трогаем

        float progress = (float)collected / required;

        // Меняем только прозрачность сегмента в зависимости от прогресса
        Color baseCol = segments[index].baseColor;
        segments[index].segment.color = new Color(
            baseCol.r,
            baseCol.g,
            baseCol.b,
            Mathf.Lerp(0.3f, 1f, progress) // альфа от 0.3 до 1 в зависимости от собранных предметов
        );

        // (опционально) Когда прогресс полный, можно сделать ярче цвет
        if (collected >= required)
        {
            segments[index].segment.color = segments[index].activeColor;
        }
    }



    int GetFormIndex(string formName)
    {
        return formName switch
        {
            "Default" => 0,
            "Iron" => 1,
            "Ice" => 2,
            "Fire" => 3,
            _ => -1
        };
    }
}