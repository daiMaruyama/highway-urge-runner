using UnityEngine;

namespace HighwayUrge.Spawning
{
    /// <summary>
    /// 障害物・アイテムのスポーンを管理する交通生成AI
    /// SpawnProfile に基づきルールベースでスポーンを制御する
    /// </summary>
    public class SpawnDirector : MonoBehaviour
    {
        // TODO: SpawnProfile 参照
        // TODO: ObjectPool（車・トラック・アイテム）
        // TODO: Awake() でプール初期化
        // TODO: Update() でルール判定・スポーン
        // TODO: ルール1: 全車線同時封鎖禁止
        // TODO: ルール2: 密集ウェーブ後に息継ぎ区間
        // TODO: ルール3: SA手前200mで交通量増加（左車線に隙間保証）
        // TODO: ルール4: 尿意75%以上でボンタン飴出現率UP
    }
}
