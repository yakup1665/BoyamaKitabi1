using UnityEngine;

public class DrawWhitMouse : MonoBehaviour
{
    // 💡 PREFAB BAĞLANTISI
    [Header("Gerekli Bağlantılar")]
    [Tooltip("Project panelinden LinePrefab'i buraya sürükleyin.")]
    [SerializeField]
    private GameObject linePrefab;

    // 💡 ÇİZİM AYARLARI
    [Header("Fırça Ayarları")]
    [Tooltip("Çizginin kalınlığı.")]
    [SerializeField]
    private float lineWidth = 0.05f;

    [Tooltip("Çizginin rengi.")]
    [SerializeField]
    private Color brushColor = Color.red; // Rengi buradan değiştirebilirsiniz

    [Tooltip("Çizgi ne kadar pürüzsüz olsun? (Yüksek değer = daha yuvarlak)")]
    [SerializeField]
    private int cornerSmoothness = 5;

    [Tooltip("Yeni nokta ekleme hassasiyeti (Düşük = daha detaylı).")]
    [SerializeField]
    private float minDistance = 0.1f;

    // 💡 TEKNİK AYARLAR
    [Header("Kamera Ayarları")]
    [Tooltip("Farenin kameradan uzaklığı (Genelde 10 iyidir).")]
    [SerializeField]
    private float zDepth = 10f;

    // O an çizilen çizgi
    private DrawingLine currentLine;

    private void Update()
    {
        // 1. Tıklama Başladığında (Yeni Çizgi)
        if (Input.GetMouseButtonDown(0))
        {
            // Çizgi objesini oluştur
            GameObject newLineObject = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
            currentLine = newLineObject.GetComponent<DrawingLine>();

            // Güvenlik kontrolü: Script veya LineRenderer eksikse ekle
            if (currentLine == null)
            {
                currentLine = newLineObject.AddComponent<DrawingLine>();
                if (newLineObject.GetComponent<LineRenderer>() == null)
                    newLineObject.AddComponent<LineRenderer>();
            }

            // 🚀 TÜM AYARLARI GÖNDER (Kalınlık ve Renk Dahil)
            currentLine.Initialize(minDistance, zDepth, cornerSmoothness, lineWidth, brushColor);

            // İlk noktayı koy
            currentLine.TryAddPoint(Input.mousePosition);
        }

        // 2. Tıklama Devam Ederken (Çizmeye Devam)
        else if (Input.GetMouseButton(0) && currentLine != null)
        {
            currentLine.TryAddPoint(Input.mousePosition);
        }

        // 3. Tıklama Bırakıldığında (Bitir)
        else if (Input.GetMouseButtonUp(0))
        {
            currentLine = null; // Bağlantıyı kopar ki sonraki tıklama yeni başlasın
        }
    }

    public void SetBrushColor(Color newColor)
    {
        brushColor = newColor; // brushColor değişkenini günceller
    }
    public void SetColorToRed() // KIRMIZI Butonu için çağrılacak
    {
        // Rengi doğrudan kırmızı olarak ayarlar
        SetBrushColor(Color.red);
    }

    public void SetColorToYellow() // SARI Butonu için çağrılacak
    {
        // Rengi doğrudan sarı olarak ayarlar
        SetBrushColor(Color.yellow);
    }
    public void SetColorToGreen() // YEŞİL Butonu için çağrılacak
    {
        // Rengi doğrudan sarı olarak ayarlar
        SetBrushColor(Color.green);
    }
}