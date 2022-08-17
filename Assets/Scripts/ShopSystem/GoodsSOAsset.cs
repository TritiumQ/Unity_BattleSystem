using UnityEngine;
public class GoodsSOAsset : ScriptableObject
{
	[Header("商品名")]
	public string GoodsName;
	[Header("商品描述")]
	public string GoodsDescription;
	[Header("商品效果名, 必须在GoodsEffectManage中具有同名的方法")]
	public string GoodsEffectName;
	public int GoodsRnak;
	public int GoodsPrice;
	public Sprite GoodsImage;
}