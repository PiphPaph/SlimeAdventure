using System.Text;

public static class DovahTransliterator
{
    public static string Transliterate(string input)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in input.ToLower())
        {
            switch (c)
            {
                case 'а': sb.Append('a'); break;
                case 'б': sb.Append('b'); break;
                case 'в': sb.Append('v'); break;
                case 'г': sb.Append('g'); break;
                case 'д': sb.Append('d'); break;
                case 'е':
                case 'ё': sb.Append('e'); break;
                case 'ж': sb.Append('j'); break;
                case 'з': sb.Append('z'); break;
                case 'и': sb.Append('i'); break;
                case 'й': sb.Append('y'); break;
                case 'к': sb.Append('k'); break;
                case 'л': sb.Append('l'); break;
                case 'м': sb.Append('m'); break;
                case 'н': sb.Append('n'); break;
                case 'о': sb.Append('o'); break;
                case 'п': sb.Append('p'); break;
                case 'р': sb.Append('r'); break;
                case 'с': sb.Append('s'); break;
                case 'т': sb.Append('t'); break;
                case 'у': sb.Append('u'); break;
                case 'ф': sb.Append('f'); break;
                case 'х': sb.Append('h'); break;
                case 'ц':
                case 'ч': sb.Append('c'); break;
                case 'ш':
                case 'щ': sb.Append('s'); break;
                case 'ъ':
                case 'ь': sb.Append('\''); break;
                case 'ы': sb.Append('y'); break;
                case 'э': sb.Append('e'); break;
                case 'ю': sb.Append("yu"); break;
                case 'я': sb.Append("ya"); break;
                case ' ': sb.Append(' '); break;
                default: sb.Append(c); break;
            }
        }

        return sb.ToString().ToUpper(); // Приводим всё к заглавным латинским буквам
    }
}
