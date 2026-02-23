using UnityEngine;

namespace HighwayUrge.Core
{
    /// <summary>
    /// ゲーム全体を管理するシングルトン
    /// GameState の管理・画面遷移・ステージ開始/終了を担う
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // TODO: Singleton 実装
        // TODO: GameState フィールド
        // TODO: Awake() で Singleton 初期化
        // TODO: StartGame()
        // TODO: ClearGame()
        // TODO: GameOver()
        // TODO: ChangeState(GameState newState)
        static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var previous = FindFirstObjectByType<GameManager>();
                    if (previous)
                    {
                        Debug.LogWarning("Initialized twice. Don't use GameManager in the scene hierarchy.");
                        _instance = (GameManager)previous;
                    }
                    else
                    {
                        var go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                        DontDestroyOnLoad(go);
                        go.hideFlags = HideFlags.HideInHierarchy;
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// ゲームの状態を表す列挙型
    /// </summary>
    public enum GameState
    {
        Title,
        Playing,
        Clear,
        GameOver
    }
}
