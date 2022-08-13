/// <summary>
/// 常量类
/// </summary>
/// <returns></returns>
public static class Const
{
    //偷懒
    /// <summary>
    /// 范围判断，左闭右闭
    /// </summary>
    /// <returns></returns>
    static bool IsInRange(int value, int left, int right)
    {
        return value >= left && value <= right;
    }

    /// <summary>
    /// 获取卡牌信息文件储存路径
    /// </summary>
    /// <returns></returns>
    public static string CARD_DATA_PATH(int _id)
    {
        if (IsInRange(_id, 0, 199))
        {
            return "CardDatas/SVN/SVN-" + _id.ToString("D3");
        }
        else if (IsInRange(_id, 200, 399))
        {
            return "CardDatas/SPL/SPL-" + _id.ToString("D3");
        }
        else if (IsInRange(_id, 500, 699))
        {
            return "CardDatas/MON/MON-" + _id.ToString("D3");
        }
        else
        {
            return null;
        }
    }
    public static string BOSS_DATA_PATH(int _id)
    {
        return "BossDatas/BOSS-" + _id.ToString("D3");
    }
    public static string PLAYER_DATA_PATH(int _id)
    {
        return UnityEngine.Application.dataPath + "/PlayerDatas/Save" + _id.ToString("D2") + ".json";
    }

    public static int INF = 0x3f3f3f3f;
    public static int MaxSaveCount = 10;
    public static string InitialCode = "mikufans";

}