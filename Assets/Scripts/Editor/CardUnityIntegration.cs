using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

static class CreateSOAssets 
{
    [MenuItem("Assets/Create/SOAsset/CardAsset")]
    public static void CreateScriptableObject1()
    {
        ScriptableObjectUtility.CreateAsset<CardAsset>();
    }
    [MenuItem("Assets/Create/SOAsset/BossAsset")]
    public static void CreateScriptableObject2()
	{
        ScriptableObjectUtility.CreateAsset<BossAsset>();
    }
}
