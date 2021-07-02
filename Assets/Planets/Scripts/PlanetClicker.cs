using UnityEngine;

public class PlanetClicker : MonoBehaviour
{
    public float ZoomScale = 100.0f;

    //private void OnMouseDown()
    //{
    //    var camera = FindObjectOfType<CameraManager>();
    //    camera.ZoomPlanet(transform, ZoomScale);
    //}

    private void OnMouseUpAsButton()
    {
        var camera = FindObjectOfType<CameraManager>();
        camera.ZoomPlanet(transform, ZoomScale);
    }
}
