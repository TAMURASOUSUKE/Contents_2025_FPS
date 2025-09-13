using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapIDManager : MonoBehaviour
{
    public enum TrapID
    {
        None, // デフォルト引数に使う(リスポーン時に必要)
        PitFall, // 落とし穴
        Needle, // 針(ステージに使うものも含め)
        BearTrap, // とらばさみ
        Rock, // 岩
        Enemy, // 敵
        Color, // フィルターの乱用
        Acid // 酸
    }
}
