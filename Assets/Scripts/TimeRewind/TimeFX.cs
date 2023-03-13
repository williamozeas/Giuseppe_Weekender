//    Copyright (C) 2020 Ned Makes Games

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
public class TimeFX : MonoBehaviour
{

    [SerializeField] private UniversalRendererData balancedRendererData;
    [SerializeField] private UniversalRendererData highFidelityRendererData;
    private UniversalRendererData rendererData = null;
    [SerializeField] private string featureName;
    [FormerlySerializedAs("transitionPeriod")] [SerializeField] private float transitionTime = 0.8f;

    private Cyan.Blit blitFeature;
    private WorldRewindRenderFeature rewindFeature;
    private Material satMat;
    private int satPropID;

    private float startTime;
    private Coroutine currentTransition;

    private static bool wor;

    private void Start() {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        if (QualitySettings.names[QualitySettings.GetQualityLevel()] == "High Fidelity")
        {
            rendererData = highFidelityRendererData;
        }
        else
        {
            rendererData = balancedRendererData;
        }
        rendererData = balancedRendererData;
        satPropID = Shader.PropertyToID("_Saturation");
        if (TryGetFeature(featureName, out var feature))
        {
            blitFeature = feature as Cyan.Blit;
            satMat = blitFeature.settings.blitMaterial;
        }
        else
        {
            Debug.LogError("Feature " + featureName + " not found!");
        }
        
        if (TryGetFeature("WorldRewindRenderFeature", out feature))
        {
            rewindFeature = feature as WorldRewindRenderFeature;
        }
        else
        {
            Debug.LogError("Feature " + featureName + " not found!");
        }
    }
    
    private bool TryGetFeature(string name, out ScriptableRendererFeature _feature) {
        _feature = rendererData.rendererFeatures.FirstOrDefault(f => f.name == name);
        return _feature != null;
    }

    public void StartRewind() {
        if (blitFeature)
        {
            if (currentTransition != null)
            {
                StopCoroutine(currentTransition);
            }

            currentTransition = StartCoroutine(RewindOn());
        }
        else
        {
            Debug.LogWarning("no blit feature!");
        }
    }
    
    public void StopRewind() {
        if (blitFeature)
        {
            if (currentTransition != null)
            {
                StopCoroutine(currentTransition);
            }
            currentTransition = StartCoroutine(RewindOff());
        }
        else
        {
            Debug.LogWarning("no blit feature!");
        }
    }

    public IEnumerator RewindOn()
    {
        rewindFeature.SetActive(true);
        blitFeature.SetActive(true);
        rendererData.SetDirty();
        
        float timeElapsed = 0;
        float start = satMat.GetFloat(satPropID);
        Debug.Log(start);
        while (timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;
            float lerp = Mathf.Lerp(start, 0f, timeElapsed/transitionTime);
            satMat.SetFloat(satPropID, lerp);
            yield return null;
        }
    }
    
    public IEnumerator RewindOff()
    {
        float timeElapsed = 0;
        float start = satMat.GetFloat(satPropID);
        while (timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;
            float lerp = Mathf.Lerp(start, 1f, timeElapsed/transitionTime);
            satMat.SetFloat(satPropID, lerp);
            Debug.Log(satMat.GetFloat(satPropID));
            yield return null;
        }
        
        
        blitFeature.SetActive(false);
        rewindFeature.SetActive(GameManager.Instance.Player.IsRewindingPlayer || GameManager.Instance.Player.IsRewindingPlayer);
        rendererData.SetDirty();
    }

    public void OnApplicationQuit()
    {
        blitFeature.SetActive(false);
        rewindFeature.SetActive(false);
    }
}