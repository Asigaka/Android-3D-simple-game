using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Presets/WavesPreset")]
public class WavesPreset : ScriptableObject
{
    [SerializeField] private List<Wave> waves;

    public List<Wave> Waves { get => waves; }
}
