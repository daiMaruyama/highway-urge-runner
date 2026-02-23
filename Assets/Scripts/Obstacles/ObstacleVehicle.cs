using System;
using UnityEngine;

namespace HighwayUrge.Obstacles
{
    /// <summary>
    /// 障害物の車両
    /// 移動・当たり判定・オブジェクトプールへの返却を担う
    /// </summary>
    public class ObstacleVehicle : MonoBehaviour
    {
        // TODO: 移動速度
        // TODO: 画面外判定のY座標（despawnY）
        // TODO: プールに戻すコールバック（Action<ObstacleVehicle>）
        // TODO: Initialize(int laneIndex, Action<ObstacleVehicle> despawnCallback)
        // TODO: Update() で下方向に移動、画面外でプールに戻す
        // TODO: OnTriggerEnter2D でプレイヤーと衝突判定
    }
}
