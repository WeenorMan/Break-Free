using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public IEnumerator Fade(float start, float end, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, time/duration);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}
