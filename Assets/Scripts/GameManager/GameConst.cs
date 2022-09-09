
/// <summary>
/// 游戏局内全局常量
/// </summary>
/// <returns></returns>
public static class GameConst
{
    public static int[] GameEventCount = { 0, 5, 6, 7, 8 };//初始化每层关卡数量（1-4）
    public static int[] level_1 = { 1, 1, 3, 3, 4 };
    public static int[] level_2 = { 1, 1, 3, 3, 4, 2 };
    public static int[] level_3 = { 1, 1, 1, 2, 3, 3, 5 };
    public static int[] level_4 = { 1, 1, 1, 3, 3, 4, 2, 5 };

    public static int[] DrawCardWeight = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
    public static int[,] Card_SVN = { { 0, 0 }, { 1, 23 }, { 50, 78 }, { 100, 117 }, { 150, 159 } };
    public static int[,] Card_SPL = { { 0, 0 }, { 201, 201 }, { 250, 267 }, { 300, 303 }, { 350, 352 } };
    public static int[,] CardRange = { { 1, 23 }, { 50, 78 }, { 100, 117 }, { 150, 159 }, { 201, 201 }, { 250, 267 }, { 300, 303 }, { 350, 352 } };


    public static int[] GetArrray(int[] _target)
    {
        int[] copy = (int[])_target.Clone();
        return copy;
    }
}
