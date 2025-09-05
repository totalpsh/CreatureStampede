# Creature Stampede

## 🎮 게임 소개

**Creature Stampede**는 끊임없이 몰려오는 적들을 상대로 생존하는 **뱀파이어 서바이버즈 스타일의 로그라이크 액션 게임**입니다. 플레이어는 다양한 능력을 조합하여 캐릭터를 성장시키고, 최후의 1인이 될 때까지 살아남아야 합니다.

---

## 🕹️ 게임 플레이 방식 (How to Play)

- **이동**: `W`, `A`, `S`, `D` 또는 방향키( `↑`, `↓`, `←`, `→` )를 사용하여 캐릭터를 움직일 수 있습니다.
- **대쉬**: `Spacebar`를 눌러 짧은 거리를 빠르게 회피하거나 이동할 수 있습니다.
- **능력(카드) 선택**: 레벨업 시 제시되는 3개의 능력 카드 중 원하는 것을 선택하여 캐릭터를 강화할 수 있습니다. `1`, `2`, `3` 숫자 키를 누르거나 마우스로 직접 카드를 클릭하여 선택하세요.

---

## ⚙️ 게임 구조 (Game Structure)

이 게임은 유연하고 확장 가능한 구조를 목표로 설계되었습니다. 핵심 관리 시스템은 싱글톤(Singleton) 패턴으로 구현되어 어디서든 쉽게 접근할 수 있습니다.

### 1. 씬 관리 시스템 (Scene Management)

`SceneLoadManager`는 게임의 전체적인 흐름을 관리하며, 각 씬(Scene)의 로딩과 전환을 담당합니다.

- **주요 씬 구성**:
  - `IntroScene`: 게임 시작 시 보여지는 인트로 씬
  - `BattleScene`: 실제 전투가 벌어지는 메인 게임 씬
- **비동기 씬 로딩**: `Coroutine`을 활용하여 씬을 비동기적으로 로드합니다. 이를 통해 씬 전환 시 게임이 멈추는 현상을 방지하고 부드러운 사용자 경험을 제공합니다.
- **씬 라이프사이클 관리**: 모든 씬은 `SceneBase`를 상속받아 `OnSceneEnter` (씬 진입 시), `OnSceneExit` (씬 이탈 시)와 같은 명확한 라이프사이클 메서드를 가집니다.

```csharp
// SceneLoadManager.cs
public class SceneLoadManager : Singleton<SceneLoadManager>
{
    // 씬 종류를 Enum으로 관리
    private Dictionary<SceneType, SceneBase> _scenes = new ();

    // 지정된 타입의 씬을 비동기로 로드
    public void LoadScene(SceneType sceneType)
    {
        // ...
        StartCoroutine(LoadSceneProcess(sceneType));
    }
}
```

### 2. UI 관리 시스템 (UI Management)

`UIManager`는 게임 내 모든 UI 요소(팝업, HUD, 버튼 등)를 생성하고 관리하는 중앙 시스템입니다.

- **UI 프리팹 관리**: 모든 UI는 프리팹으로 만들어지며, `UIManager`가 필요에 따라 동적으로 생성하거나 가져옵니다.
- **UI 라이프사이클**: 모든 UI는 `UIBase`를 상속받아 `OpenUI()`, `CloseUI()`와 같은 공통 인터페이스를 제공합니다.
- **리소스 관리**: 씬이 전환될 때 `UIManager`는 해당 씬에서 사용된 UI를 정리하고, `Resources.UnloadUnusedAssets()`를 호출하여 메모리를 효율적으로 관리합니다.

```csharp
// UIManager.cs
public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, UIBase> _uiDictionary = new Dictionary<string, UIBase>();

    // 제네릭(Generic)을 사용하여 특정 UI를 열거나 닫음
    public void OpenUI<T>() where T : UIBase { /* ... */ }
    public void CloseUI<T>() where T : UIBase { /* ... */ }
}
```

### 3. 리소스 및 데이터 관리

게임에 필요한 각종 리소스(캐릭터, 맵, UI 프리팹 등)의 경로를 `Constants.cs` 파일에서 중앙 관리하여 휴먼 에러를 줄이고 유지보수성을 높였습니다.

```csharp
// Common/Constants.cs
public static class Path
{
    public const string Prefab = "Prefab/";
    public const string UI = Prefab + "UI/";
    public const string Character = Prefab + "Character/";
    public const string Map = Prefab + "Map/";
}
```

---

## ✨ 게임 기능 (Game Features)

현재 구현된 구조를 바탕으로 다음과 같은 핵심 기능들을 구현할 수 있습니다.

### 1. 생존 전투 (Survival Combat)

- **Player / Enemy**: `BattleScene`에서 플레이어와 다수의 적(`Enemy`)이 등장합니다. 플레이어는 이들로부터 최대한 오래 살아남아야 합니다.
- **점수 시스템**: 생존 시간이나 처치한 적의 수에 따라 점수(`Score`)를 기록할 수 있습니다.

### 2. 동적 UI 시스템

- 게임 상황에 따라 필요한 UI(레벨업 선택창, 게임 오버 팝업, 설정창 등)를 동적으로 띄우고 닫을 수 있습니다.
- UI 요소들은 재사용이 가능하도록 슬롯(Slot) 형태로도 생성할 수 있습니다.

### 3. 스테이지 확장성

- `introScene`과 `BattleScene`이 분리되어 있어, 향후 새로운 스테이지(`Stage`)나 전투 모드를 추가하기 용이합니다.
- 각 씬은 독립적으로 구성되어 있어 새로운 기능을 추가하거나 기존 기능을 수정할 때 다른 씬에 미치는 영향을 최소화할 수 있습니다.
