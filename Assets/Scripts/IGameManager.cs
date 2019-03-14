using UnityEngine;

public interface IGameManager
{
    Mission CurrentMission { get; }
    void AddScoreForRebound(Vector3 worldPosition);
    void AddScoreForWalls(Vector3 worldPosition);
    void PauseGame();
    void ResumeGame();
    void ProcessResults();
    void TriggerGameOver();
    void ResetGame();
}