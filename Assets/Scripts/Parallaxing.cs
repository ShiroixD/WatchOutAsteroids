using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] Backgrounds;
    private float[] _parallaxScales;
    public float Smoothing = 1f;

    private Transform _cam;
    private Vector3 _previousCamPos;

    void Awake()
    {
        _cam = Camera.main.transform;
    }

    void Start()
    {
        _previousCamPos = _cam.position;
        _parallaxScales = new float[Backgrounds.Length];

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            _parallaxScales[i] = -Backgrounds[i].position.z;
        }
    }

    void Update()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            AdjustParallaxForBackground(i);
        }

        _previousCamPos = _cam.position;
    }

    private void AdjustParallaxForBackground(int i)
    {
        float parallax = CalculateBackgroundParallax(i);
        float backgroundTargetPosX = Backgrounds[i].position.x + parallax;
        Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);
        Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
    }

    private float CalculateBackgroundParallax(int i)
    {
        return (_previousCamPos.x - _cam.position.x) * _parallaxScales[i];
    }

    private Vector3 GetAdjustedBackgroundPosition(int i, Vector3 backgroundTargetPos)
    {
        return Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
    }
}