using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public Transform _TransformToShake;

    private float shakeDuration = 1f;
    private float shakeAmount = 0.7f;

    Vector3 originalPos;

    public void Ini(float _ShakeDuration, float _ShakeAmout)
    {
        shakeDuration = _ShakeDuration;
        shakeAmount = _ShakeAmout;
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        originalPos = _TransformToShake.position;
        for (float t = 1f; t > 0; t -= Time.deltaTime / shakeDuration)
        {
            _TransformToShake.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            yield return null;
        }
        _TransformToShake.position = originalPos;
    }
}
