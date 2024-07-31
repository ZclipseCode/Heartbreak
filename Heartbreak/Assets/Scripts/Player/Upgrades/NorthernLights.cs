using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthernLights : Upgrade
{
    FreezeDebuff freezeDebuff;
    GameObject trailPrefab;
    float trailFollowTime = 1f;
    float trailDestroyTime = 1f;

    private void Awake()
    {
        upgradeType = UpgradeType.dodge;

        freezeDebuff = GetComponent<FreezeDebuff>();

        if (freezeDebuff == null)
        {
            freezeDebuff = gameObject.AddComponent<FreezeDebuff>();
        }

        trailPrefab = GetComponent<UpgradeController>().GetNorthernLightsTrailPrefab();
    }

    public override void PerformEffects()
    {
        StartCoroutine(ActivateTrail());
    }

    IEnumerator ActivateTrail()
    {
        GameObject trail = Instantiate(trailPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z), Quaternion.identity, gameObject.transform);

        yield return new WaitForSeconds(trailFollowTime);

        trail.transform.SetParent(null);

        TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();
        StartCoroutine(FadeTrail(trailRenderer));

        yield return new WaitForSeconds(trailDestroyTime);

        Destroy(trail);
    }

    IEnumerator FadeTrail(TrailRenderer trailRenderer)
    {
        float targetAlpha = 0f;

        // get the current color gradient
        Gradient gradient = trailRenderer.colorGradient;
        GradientColorKey[] colorKeys = gradient.colorKeys;
        GradientAlphaKey[] currentAlphaKeys = gradient.alphaKeys;

        // determine the starting alpha keys based on current state
        GradientAlphaKey[] startAlphaKeys = new GradientAlphaKey[currentAlphaKeys.Length];
        for (int i = 0; i < currentAlphaKeys.Length; i++)
        {
            startAlphaKeys[i] = currentAlphaKeys[i];
        }

        // define the end alpha keys with the target alpha
        GradientAlphaKey[] endAlphaKeys = new GradientAlphaKey[currentAlphaKeys.Length];
        for (int i = 0; i < currentAlphaKeys.Length; i++)
        {
            endAlphaKeys[i] = new GradientAlphaKey(targetAlpha, currentAlphaKeys[i].time);
        }

        // time tracking
        float elapsedTime = 0f;

        while (elapsedTime < trailDestroyTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / trailDestroyTime;

            // lerp the alpha values
            GradientAlphaKey[] lerpedAlphaKeys = new GradientAlphaKey[currentAlphaKeys.Length];
            for (int i = 0; i < currentAlphaKeys.Length; i++)
            {
                lerpedAlphaKeys[i] = new GradientAlphaKey(
                    Mathf.Lerp(startAlphaKeys[i].alpha, endAlphaKeys[i].alpha, t),
                    startAlphaKeys[i].time
                );
            }

            // create a new gradient with the lerped alpha keys
            Gradient newGradient = new Gradient();
            newGradient.SetKeys(colorKeys, lerpedAlphaKeys);

            // assign the new gradient to the TrailRenderer
            trailRenderer.colorGradient = newGradient;

            yield return null;
        }

        // ensure the alpha is set to the target value at the end
        trailRenderer.colorGradient = new Gradient
        {
            colorKeys = colorKeys,
            alphaKeys = endAlphaKeys
        };
    }
}
