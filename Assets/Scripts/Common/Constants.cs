public enum SceneType
{
    Intro,
    Town,
    Battle
}

public static class Path
{
    public const string Prefab = "Prefab/";
    public const string UI = Prefab + "UI/";
    public const string Character = Prefab + "Character/";
    public const string Map = Prefab + "Map/";
}
    
public static class Prefab
{
    // Character
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    
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