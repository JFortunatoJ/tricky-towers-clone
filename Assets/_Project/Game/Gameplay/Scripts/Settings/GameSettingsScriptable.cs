using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Settings/Game Settings")]
public class GameSettingsScriptable : ScriptableObject
{
    public GameSettings gameSettings;
}

[Serializable]
public class GameSettings
{
    public LevelSettings levelSettings;
    public PieceSettings pieceSettings;
}

[Serializable]
public class LevelSettings
{
    [Range(10, 80)]
    public int finishLineHeight = 40;
    [Range(1, 7)]
    public int piecesLostToGameOver = 5;
}

[Serializable]
public class PieceSettings
{
    [Min(1)]
    public float gravityMultiplier = 1;
    [Range(.1f, 1f)]
    public float horizontalStep = 0.5f;
    [Range(1, 5)]
    public float normalDescendSpeed = 4f;
    [Range(6, 20)]
    public float boostDescendSpeed = 12f;
}