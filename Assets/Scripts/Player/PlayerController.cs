using HighwayUrge.Core;
using UnityEngine;

namespace HighwayUrge.Player
{
    /// <summary>
    /// プレイヤーの車を操作する
    /// 入力受付・車線変更・当たり判定を担う
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        // DONE: 現在の車線番号（0=左, 1=中, 2=右）
        // DONE: 移動速度
        // TODO: イベント定義（OnItemCollected, OnSAEntered, OnObstacleHit）
        // DONE: Awake() / Start()
        // DONE: Update() で入力検出
        // DONE: ChangeLane(int direction)
        // DONE: OnTriggerEnter2D(Collider2D other)
        [SerializeField] float[] _lanePositionsX = { -3f, 0f, 3f };
        [SerializeField] float[] _laneSpeeds = { 3f, 5f, 8f }; // 左・中・右

        int _currentLane = 1;   // 初期値：中央
        bool _isMoving = false; // 連打防止
        GameManager GM => GameManager.Instance;
        void Awake()
        {
            SnapToLane();
        }
        void Update()
        {
            if (GM.CurrentState != GameState.Playing) return;

            if (Input.GetKeyDown(KeyCode.LeftArrow)) TryChangeLane(-1);
            if (Input.GetKeyDown(KeyCode.RightArrow)) TryChangeLane(1);
        }
        public void TryChangeLane(int direction)
        {
            // -1: 左, +1: 右
            // 端だったら何もしない方向ある
            if (_currentLane == 0 && direction == -1) return;
            if (_currentLane == _lanePositionsX.Length - 1 && direction == 1) return;
            if (_isMoving) return; // 連打防止
            _currentLane += direction;
            SnapToLane();
        }

        void SnapToLane()
        {
            var pos = transform.position;
            pos.x = _lanePositionsX[_currentLane];
            transform.position = pos;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Drink"))
            {
                // イベント発火
                Debug.Log("OMG");
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Bontan"))
            {
                // イベント発火
                Debug.Log("Calm Down");
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("SA"))
            {
                // SA 進入イベント発火
                if (_currentLane != 0) return; // SA は左車線のみを一応入れる
                Debug.Log("Entered SA!");
            }
            else if (other.CompareTag("Obstacle"))
            {
                // 障害物ヒットイベント発火
                Debug.Log("Hit Obstacle!");
                GM.GameOver();
            }
        }
    }
}
