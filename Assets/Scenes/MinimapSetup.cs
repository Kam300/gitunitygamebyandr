using UnityEngine;
using UnityEngine.UI;

public class MinimapSetup : MonoBehaviour
{
    public Camera minimapCamera; // ������, ������� ������� ���������
    public RawImage minimapDisplay; // UI �������, ��� ������������ ���������
    private RenderTexture minimapTexture;

    void Start()
    {
        // ������� ����� RenderTexture
        minimapTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32)
        {
            antiAliasing = 1,
            filterMode = FilterMode.Bilinear
        };
        minimapTexture.Create();

        // ����������� ������ ���������
        if (minimapCamera != null)
        {
            minimapCamera.targetTexture = minimapTexture;
            minimapCamera.clearFlags = CameraClearFlags.SolidColor;
            minimapCamera.backgroundColor = Color.black; // ��� ����� ������ ���� ����
            minimapCamera.allowMSAA = false;
            minimapCamera.allowHDR = false;
        }

        // ��������� �������� UI ��������
        if (minimapDisplay != null)
        {
            minimapDisplay.texture = minimapTexture;
        }
    }

    void OnDestroy()
    {
        if (minimapTexture != null)
        {
            minimapTexture.Release();
            Destroy(minimapTexture);
        }
    }
}