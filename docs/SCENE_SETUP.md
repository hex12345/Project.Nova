# Scene Setup

The included `scenes/Main.tscn` is a runnable skeleton with:

- `GameManager`
- `HybridSceneDirector`
- `World3DRoot`
- `UI2DRoot`
- `DebateLayer`
- `VNLayer`
- `TimerHud`
- `FocusHud`
- `LogicDiveRunner`

## Recommended Godot setup

1. Open `scenes/Main.tscn`.
2. Make sure `GameManager` script is attached.
3. Make sure `HybridSceneDirector` script is attached.
4. Add real meshes to `World3DRoot/TubeRoot`.
5. Add real Control styling/theme to `UI2DRoot`.
6. Add legal character PNGs and WAV files.

## Branch gate placement

Place three `BranchGate` nodes in front of the runner:

```txt
LeftGate   -> wrong/correct answer
CenterGate -> wrong/correct answer
RightGate  -> wrong/correct answer
```

The data file decides what text and correctness each gate represents.
