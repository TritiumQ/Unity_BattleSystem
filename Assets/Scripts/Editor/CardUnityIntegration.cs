using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

static class CreateSOAssets 
{
    [MenuItem("Assets/Create/SOAsset/CardAsset")]
    public static void CreateScriptableObject1()
    {
        ScriptableObjectUtility.CreateAsset<CardSOAsset>();
    }
    [MenuItem("Assets/Create/SOAsset/BossAsset")]
    public static void CreateScriptableObject2()
	{
        ScriptableObjectUtility.CreateAsset<BossSOAsset>();
    }
    [MenuItem("Assets/Create/SOAsset/TestAsset")]
    public static void CreateScriptableObject3()
    {
        ScriptableObjectUtility.CreateAsset<TestAsset>();
    }
    [MenuItem("Assets/Create/SOAsset/GoodsAsset")]
    public static void CreateScriptableObject4()
    {
        ScriptableObjectUtility.CreateAsset<GoodsSOAsset>();
    }
}
