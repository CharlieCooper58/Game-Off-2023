using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EggTrackerUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text UITextMeshPro;
    [SerializeField]
    Vector3 offset;
    public Spawner spawner;
    RectTransform HUDRect;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AssignSpawner(Spawner sp) 
    {
        spawner = sp;
        HUDRect = transform.parent.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (spawner == null || !spawner.enabled) return;
        if (UITextMeshPro != null)//We disabled the distance display.
        UITextMeshPro.text = Vector3.Magnitude(spawner.transform.position - Camera.main.transform.position).ToString("0.00");
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(spawner.transform.position);
        

        //If the spawner is on screen
        if (viewportPos.x >= 0f && viewportPos.x <= 1f && viewportPos.y >= 0f && viewportPos.y <= 1f && viewportPos.z > 0f)
            OnScreenRepositionMarker();
        else
            OffScreenRepositionMarker(viewportPos.z);



    }
    void OnScreenRepositionMarker() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(spawner.transform.position+offset);
        float markerX = (screenPos.x / Screen.width - 0.5f) * HUDRect.rect.width;
        float markerY = (screenPos.y / Screen.height - 0.5f) * HUDRect.rect.height;
        (transform as RectTransform).anchoredPosition = new Vector2(markerX, markerY);
        (transform as RectTransform).rotation = Quaternion.LookRotation(Vector3.forward);
    }
    void OffScreenRepositionMarker(float z) {
        float distanceFromCenter = 0.2f * Mathf.Sqrt(HUDRect.rect.width* HUDRect.rect.width+ HUDRect.rect.height* HUDRect.rect.height);
        Vector3 vecToTarget = spawner.transform.position - Camera.main.transform.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(spawner.transform.position);
        float markerX = screenPos.x / Screen.width - 0.5f;
        float markerY = screenPos.y / Screen.height - 0.5f;
        float magnitude = Mathf.Sqrt(markerX * markerX + markerY * markerY);
        float markerXFinal = markerX / magnitude * distanceFromCenter * Mathf.Sign(z);
        float markerYFinal = markerY / magnitude * distanceFromCenter * Mathf.Sign(z);
        (transform as RectTransform).anchoredPosition = new Vector2(markerXFinal, markerYFinal);
        (transform as RectTransform).rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(-markerXFinal, -markerYFinal, 0));
    }
}
