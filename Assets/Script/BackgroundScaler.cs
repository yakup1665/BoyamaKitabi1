using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        // Sprite Renderer bileþenini al
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Kameranýn OrthographicSize (görünüm yüksekliðinin yarýsý)
        float worldScreenHeight = Camera.main.orthographicSize * 2f;

        // Kameranýn OrthographicSize ve Aspect'e göre hesaplanan dünya geniþliði
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // Resmin (Sprite'ýn) dünya birimlerindeki mevcut geniþlik ve yüksekliðini bul
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        // 1. Ölçekleme Faktörlerini Hesapla (Resmi tam kaplayacak en büyük oraný bul)
        // Eðer resim dikeyde dar kalýyorsa, dikeyde büyüt. Eðer yatayda dar kalýyorsa, yatayda büyüt.
        float scaleX = worldScreenWidth / spriteWidth;
        float scaleY = worldScreenHeight / spriteHeight;

        // 2. Kaplama için büyük olan ölçeði seç (cover mode)
        float finalScale = Mathf.Max(scaleX, scaleY);

        // 3. Ölçeði Uygula
        transform.localScale = new Vector3(finalScale, finalScale, 1f);

        // 4. Pozisyonu ortala
        transform.position = new Vector3(0f, 0f, transform.position.z);
    }
}