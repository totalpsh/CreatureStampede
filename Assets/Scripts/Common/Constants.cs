public enum SceneType
{
    Intro,
    Town,
    Battle,
    PlayerTest,
    Dungeon,

}

public static class Path
{
    public const string Prefab = "Prefab/";
    public const string UI = Prefab + "UI/";
    public const string Character = Prefab + "Character/";
    public const string Monster = Prefab + "Monster/";
    public const string Item = Prefab + "Item/";
    public const string Map = Prefab + "Map/";
    public const string Camera = Prefab + "Camera/";
    public const string Data = "Data/";
    public const string Sound = "Sound/";
}
    
public static class Prefab
{
    // Character
    public const string Player = "Player";
    public const string Enemy = "Enemy";

    // camera
    public const string VirtualCamera = "VirtualCamera";

    // Map
    public const string Stage = "Stage";
    public const string Town = "Town";
    
    // UI
    public const string Canvas = "Canvas";
    public const string EventSystem = "EventSystem";
}

public static class PrefKey
{
    public const string Score = "Score";
    
}

// 나중에 범위가 커진다면 클래스파일 분리해도 좋음
public static class MonsterAnimParam
{
    public const string MoveX = "MoveX";
    public const string MoveY = "MoveY";

    public const string IsMoving = "IsMoving";
    public const string IsHit = "IsHit";

    public const string Attack = "Attack";
    public const string Die = "Die";
    public const string Hit = "Hit";
}