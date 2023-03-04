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
    private Material satMat;
    private int satPropID;

    private float startTime;
    private Coroutine currentTransition;

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
        if (TryGetFeature(out var feature))
        {
            blitFeature = feature as Cyan.Blit;
            satMat = blitFeature.settings.blitMaterial;
        }
        else
        {
            Debug.LogError("Feature " + featureName + " not found!");
        }
    }

    // private void OnDisable() {
    // }

    private bool TryGetFeature(out ScriptableRendererFeature _feature) {
        // foreach (var feat in rendererData.rendererFeatures)
        // {
            // Debug.Log(rendererData);
        // }
        _feature = rendererData.rendererFeatures.FirstOrDefault(f => f.name == featureName);
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
        rendererData.SetDirty();
    }

    // private void UpdateTransition() {
    //     if(TryGetFeature(out feature)) {
    //         float saturation = Mathf.Clamp01((Time.timeSinceLevelLoad - startTime) / transitionPeriod);
    //
    //         var blitFeature = feature as RewindFeature;
    //         var material = blitFeature.Material;
    //         material.SetFloat("_Saturation", saturation);
    //     }
    // }
    //
    // private void ResetTransition() {
    //     if(TryGetFeature(out var feature)) {
    //         feature.SetActive(true);
    //         rendererData.SetDirty();
    //
    //         var blitFeature = feature as RewindFeature;
    //         var material = blitFeature.Material;
    //         material.SetFloat("_Saturation", 0);
    //         
    //         transitioning = false;
    //     }
    // }
}