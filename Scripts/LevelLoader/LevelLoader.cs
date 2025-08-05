using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public TextAsset levelFile; // Drag and drop level1.txt here
    public float tileSize = 1f;

    void Start()
    {
        LoadLevel();
    }

    void LoadLevel()
    {
        string[] lines = levelFile.text.Split('\n');

        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[lines.Length - 1 - y]; // Снизу вверх

            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                Vector2 position = new Vector2(x * tileSize, y * tileSize);

                switch (c)
                {
                    case 'F':
                        Instantiate(Resources.Load("Prefabs/Construction/Floor"), position, Quaternion.identity);
                        break;
                    case 'P':
                        Instantiate(Resources.Load("Prefabs/Construction/Platform"), position, Quaternion.identity);
                        break;
                    case 'W':
                        Instantiate(Resources.Load("Prefabs/Construction/Wall"), position, Quaternion.identity);
                        break;
                    case 'C':
                        Instantiate(Resources.Load("Prefabs/Construction/Celling"), position, Quaternion.identity);
                        break;
                }
            }
        }
    }
}