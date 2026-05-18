using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class FitSpriteToCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private bool preserveAspectRatio = true;
    [SerializeField] private bool followCameraPosition = true;
    [SerializeField] private int sortingOrder = -100;

    private SpriteRenderer spriteRenderer;
    private int lastScreenWidth;
    private int lastScreenHeight;
    private float lastCameraSize;
    private float lastCameraAspect;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        FitToCamera();
    }

    private void LateUpdate()
    {
        Camera cameraToFit = GetCamera();
        if (cameraToFit == null) return;

        if (Screen.width != lastScreenWidth ||
            Screen.height != lastScreenHeight ||
            !Mathf.Approximately(cameraToFit.orthographicSize, lastCameraSize) ||
            !Mathf.Approximately(cameraToFit.aspect, lastCameraAspect))
        {
            FitToCamera();
        }

        if (followCameraPosition)
        {
            transform.position = new Vector3(
                cameraToFit.transform.position.x,
                cameraToFit.transform.position.y,
                transform.position.z);
        }
    }

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        FitToCamera();
    }

    private void FitToCamera()
    {
        Camera cameraToFit = GetCamera();
        if (cameraToFit == null || spriteRenderer == null || spriteRenderer.sprite == null) return;

        spriteRenderer.sortingOrder = sortingOrder;

        float screenHeight = cameraToFit.orthographicSize * 2f;
        float screenWidth = screenHeight * cameraToFit.aspect;
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        if (preserveAspectRatio)
        {
            float scale = Mathf.Max(screenWidth / spriteSize.x, screenHeight / spriteSize.y);
            transform.localScale = new Vector3(scale, scale, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(
                screenWidth / spriteSize.x,
                screenHeight / spriteSize.y,
                transform.localScale.z);
        }

        if (followCameraPosition)
        {
            transform.position = new Vector3(
                cameraToFit.transform.position.x,
                cameraToFit.transform.position.y,
                transform.position.z);
        }

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        lastCameraSize = cameraToFit.orthographicSize;
        lastCameraAspect = cameraToFit.aspect;
    }

    private Camera GetCamera()
    {
        if (targetCamera != null) return targetCamera;
        return Camera.main;
    }
}
