using UnityEngine;

namespace HighwayUrge.Urge
{
    /// <summary>
    /// 尿意メーターを管理する
    /// 時間経過による上昇・アイテムによる増減・演出トリガーを担う
    /// </summary>
    public class UrgeSystem : MonoBehaviour
    {
        // TODO: Singleton 実装
        // TODO: 尿意パーセント（0〜100）
        // TODO: 上昇速度（StageDataから設定）
        // TODO: Update() で自然上昇
        // TODO: AddUrge(float amount)
        // TODO: ReduceUrge(float amount)
        // TODO: 100%到達で GameManager.GameOver() を呼ぶ
        // TODO: 75%到達で警告演出トリガー
    }
}
