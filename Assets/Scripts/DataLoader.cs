using UnityEngine;
public static class DataLoader //数据读取辅助类
{
    public static T LoadSOAsset<T>(string _path) where T : ScriptableObject
	{
		var data = Resources.Load<T>(_path);
		return (data != null) ? data : null;
	}
	public static TextAsset LoadTextAsset(string _path)
	{
		var data = Resources.Load<TextAsset>(_path);
		return (data != null) ? data : null;
	}
}

