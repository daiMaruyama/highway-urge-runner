# CLAUDE.md — Highway Urge Runner

## プロジェクト概要

高速道路を走行中、尿意が限界に達する前にSA/PAにたどり着くカジュアルドッジゲーム。
左右の車線変更だけで遊べるシンプルな操作でありながら、尿意メーターと交通状況の緊張感がプレイヤーを引きつける。

- **タイトル**: 「トイレに間に合え！」(HIGHWAY URGE RUNNER)
- **エンジン**: Unity 6
- **UI**: UI Toolkit (UXML + USS + C#)
- **ビルドターゲット**: WebGL（ブラウザ）
- **想定プレイ時間**: 30秒〜2分/ステージ

## 技術スタック

- Unity 6 / C# (.NET Standard 2.1)
- UI Toolkit（UGUI は使わない）
- WebGL ビルド（unityroom 等での公開を想定）

## フォルダ構成

```
Assets/
├── Scripts/
│   ├── Core/           # GameManager, GameState
│   ├── Player/         # PlayerController
│   ├── Spawning/       # SpawnDirector, SpawnProfile(SO)
│   ├── Urge/           # UrgeSystem
│   ├── Obstacles/      # ObstacleVehicle, ItemPickup
│   ├── Stage/          # StageData(SO), SAZone
│   └── UI/             # NavUIController, 各画面のUI制御
├── Data/
│   ├── StageData/      # ScriptableObject（ステージ定義）
│   └── SpawnProfile/   # ScriptableObject（スポーン設定）
├── UI/
│   ├── UXML/           # TitleScreen, GameHUD, ClearScreen, GameOverScreen
│   └── USS/            # CommonStyles + 各画面個別スタイル
├── Prefabs/            # PlayerCar, ObstacleCar, Truck, BontanAme, Drink, SAZone, BumpZone
├── Art/                # Sprites, Materials（フラットデザイン）
└── Scenes/
    └── Main.unity      # シングルシーン
```

## 主要クラスと責務

| クラス | 種別 | 責務 |
|--------|------|------|
| GameManager | MonoBehaviour (Singleton) | ゲームステート管理、画面遷移、ステージ開始/終了 |
| PlayerController | MonoBehaviour | 入力受付、車線変更、当たり判定 |
| SpawnDirector | MonoBehaviour | 交通生成AI。SpawnProfileに基づき障害物/アイテムを生成 |
| SpawnProfile | ScriptableObject | ステージ別のスポーンパラメータ（交通量、密集率、息継ぎ間隔等） |
| StageData | ScriptableObject | ステージ定義（車線数、速度、SA配置、尿意上昇率） |
| UrgeSystem | MonoBehaviour | 尿意メーター管理、演出トリガー |
| NavUIController | MonoBehaviour | カーナビUI の更新（SA残距離、通過表示、警告） |
| ObstacleVehicle | MonoBehaviour | 障害車両の移動・当たり判定・オブジェクトプール対応 |
| ItemPickup | MonoBehaviour | アイテム（ボンタン飴/ドリンク）の移動・収集判定 |
| SAZone | MonoBehaviour | SA/PAクリアゾーンの判定、接近アラート |

## ゲームデザインの要点

### コアメカニクス
- 2〜3車線のレーンベース移動（タップ or ←→キー）
- 尿意メーター（0%→100%で自然上昇、100%到達でゲームオーバー）
- 複数SA/PA配置（任意のSAに入ればクリア、逃すと次を目指す）

### 車線速度差（日本の高速道路準拠）
- 左車線（走行車線）: 速度×0.7、交通量多い、SA入口あり
- 中央車線: 速度×1.0、バランス型
- 右車線（追越車線）: 速度×1.3、交通量少ない、速いが危険

### スポーンディレクター（パターンテーブルではなくルールベース）
- 全車線同時封鎖禁止（必ず1車線は空ける）
- 密集ウェーブ後に強制息継ぎ区間
- SA手前200mで交通量増加（ただし左車線に隙間を保証）
- 尿意75%以上でボンタン飴出現率UP
- 平和区間が3秒以上続いたら密集ウェーブ投入

### アイテム
- 🍬 ボンタン飴: 尿意-10%（救済）
- 🥤 ドリンク: 尿意+12%（トラップ）
- でこぼこゾーン: レーン単位配置、尿意+4% + 画面揺れ

### ステージ構成
| ステージ | 車線数 | 速度 | 尿意上昇率 | SA数 |
|----------|--------|------|-----------|------|
| 一般道 | 2 | 低速 | 緩やか | 2 |
| 高速道路 | 3 | 中速 | 普通 | 3 |
| 首都高 | 3 | 高速 | 急速 | 2 |

### 画面遷移
タイトル → ゲーム → クリア or ゲームオーバー → タイトル/リトライ

## コーディング規約

- **名前空間**: `HighwayUrge` をルートとする（例: `HighwayUrge.Core`, `HighwayUrge.Spawning`）
- **命名**: PascalCase（クラス/メソッド）、camelCase（ローカル変数/フィールド）、UPPER_SNAKE（定数）
- **SerializeField**: private フィールドに `[SerializeField]` を付ける。public フィールドは避ける
- **コメント**: クラスと public メソッドには XML ドキュメントコメントを付ける
- **マジックナンバー禁止**: 数値は const か SerializeField で定義
- **MonoBehaviour**: `Awake` で初期化、`OnEnable`/`OnDisable` でイベント登録/解除
- **ScriptableObject**: 値の変更はランタイムで行わない。ランタイム用コピーが必要な場合は `Instantiate` する

## 注意事項

- UGUI (Canvas/EventSystem) は使わない。すべて UI Toolkit で実装する
- シングルシーン構成。画面遷移は GameState ステートマシン + UXML ドキュメント切り替えで行う
- WebGL ビルドを常に意識する（Update 内の GC Alloc を最小化、オブジェクトプール活用）
- 詳細な設計書は `Docs/game-design-doc.docx` を参照
