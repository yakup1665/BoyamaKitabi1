using UnityEngine;

public class DrawingLine : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;

    // Ayarlar (DrawWhitMouse'dan gelecek)
    private float minDistance;
    private float zDepth;

    // 🚀 BAŞLATMA METODU (5 Parametreli)
    public void Initialize(float distance, float depth, int smoothness, float lineWidth, Color color)
    {
        line = GetComponent<LineRenderer>();
        if (line == null) return;

        // Değerleri kaydet
        minDistance = distance;
        zDepth = depth;

        // Görünüm Ayarları (Pürüzsüzlük)
        line.numCornerVertices = smoothness;
        line.numCapVertices = smoothness;
        line.textureMode = LineTextureMode.Tile;

        // Kalınlık Ayarı
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;

        // 🎨 Renk Ayarı
        line.startColor = color;
        line.endColor = color;

        // Sıfırlama
        line.positionCount = 0;
        previousPosition = Vector3.zero;
    }

    // NOKTA EKLEME METODU
    public bool TryAddPoint(Vector3 mouseScreenPosition)
    {
        // Fareyi dünya koordinatına çevir
        mouseScreenPosition.z = zDepth;
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // 🔥 KRİTİK DÜZELTME: Çizgiyi arka planın (Z=0) azıcık önüne alıyoruz.
        // Böylece çizimler her zaman resmin üzerinde görünür.
        currentPosition.z = -0.01f;

        // İlk nokta ise direkt ekle
        if (line.positionCount == 0)
        {
            line.positionCount = 1;
            line.SetPosition(0, currentPosition);
            previousPosition = currentPosition;
            return true;
        }

        // Mesafe yeterliyse yeni nokta ekle
        if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
        {
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, currentPosition);
            previousPosition = currentPosition;
            return true;
        }

        return false;
    }
}