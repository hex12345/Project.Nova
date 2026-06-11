# Architecture

## The rule

The project uses a persistent root scene. It does not constantly destroy and
reload the entire game scene for every phase. This keeps timers, music, loaded
assets, and state machines stable across phase transitions.

## Layers

```txt
Root
├─ GameManager
├─ HybridSceneDirector
│  ├─ World3DRoot
│  ├─ UI2DRoot
│  └─ TransitionLayer
└─ AudioManager
```

## State flow

```txt
VisualNovelSceneState
        ↓
DebateSceneState
        ↓
LogicDiveSceneState
        ↓
Result or VisualNovelSceneState
```

## System responsibilities

| System | Owns | Avoids |
|---|---|---|
| GameManager | phase transitions, global context | rendering implementation |
| HybridSceneDirector | 2D/3D visibility, fade, camera switching | contradiction logic |
| LocalAssetLoader | PNG/WAV load/cache | gameplay decisions |
| ContradictionEngine | truth bullets, weak points, validation | UI animation |
| DebateTextActor | moving text visuals | math correctness |
| LogicDiveRunner | player movement and physics snapshots | trial state transitions |
| RewindBuffer | time-indexed snapshot stack | rendering |

## 3D-to-2D transition

1. `GameManager.ChangeState()` asks current state to exit.
2. `HybridSceneDirector` fades to black.
3. 2D/3D roots swap visibility.
4. Target camera becomes active.
5. Fade returns.
6. New state enters and binds its scene nodes.

This keeps global timer and focus gauge alive across phases.
