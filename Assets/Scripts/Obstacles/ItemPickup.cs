using System;
using UnityEngine;

namespace HighwayUrge.Obstacles
{
    /// <summary>
    /// アイテム（ボンタン飴・ドリンク）
    /// 移動・収集判定・効果発動を担う
    /// </summary>
    public class ItemPickup : MonoBehaviour
    {
        // TODO: アイテム種別（enum ItemType: BontanCandy, Drink）
        // TODO: プールに戻すコールバック
        // TODO: Initialize(Action<ItemPickup> despawnCallback)
        // TODO: Update() で下方向に移動
        // TODO: OnTriggerEnter2D でプレイヤーと判定
        // TODO: ApplyEffect() でアイテム効果発動（UrgeSystemへ通知）
    }
}
