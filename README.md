# DanganEdu Trial Engine

A modular Godot 4 C# starter project for a high-fidelity educational trial
engine inspired by class-trial mechanics. The project is data-driven so trial
content can be changed through JSON files instead of hardcoded scene logic.

This is an AI-assisted prototype prepared for open-source use.

- 2D Visual Novel phase
- Nonstop Debate contradiction phase
- 3D Logic Dive equation-path runner
- Global countdown timer
- Focus gauge for slow-motion debate reading
- Rewind stack for 3D physics snapshots
- Local PNG/WAV asset loading through manifests

This package intentionally includes no copyrighted Danganronpa sprites, sounds,
music, logos, or ripped assets. Add your own legal placeholder/original assets
under `res://assets_local/` or point the loader at an external directory. This
project is not affiliated with, endorsed by, or sponsored by Spike Chunsoft.

## Engine target

- Godot 4.3 with .NET support
- C# / .NET
- 2D + 3D hybrid scene architecture

## Repository layout

```txt
assets_local/   Local placeholder/original assets; committed folders are empty
data/           Editable character, audio, debate, logic-dive, and config JSON
docs/           Architecture, scene setup, and asset pipeline notes
scenes/         Godot scene files
scripts/        Python helper and sanity-check scripts
src/            C# gameplay, UI, asset, debate, VN, and logic-dive systems
```

## Main idea

The project is split into clean systems:

```txt
Core           -> phase changes, timer, focus, scene transitions
Assets         -> manifest loading, PNG/WAV cache, audio playback
Debate         -> weak points, truth bullets, moving text, contradiction resolution
LogicDive      -> 3D runner, branch gates, equation problems, rewind buffer
VN             -> dialogue sequencing and character sprites
UI             -> timer/focus HUD and fade overlay
Utils          -> JSON loading, node guard helpers, math text formatting
Data           -> editable trial scripts and problem files
Scripts        -> Python helper tools
```

## Quick start

```bash
git clone https://github.com/hex12345/Fully-ai-dangan-project.git
cd Fully-ai-dangan-project
python scripts/verify_project.py
```

Then:

1. Open this folder in Godot 4.3 with .NET support.
2. Let Godot generate/import C# metadata.
3. Open `scenes/Main.tscn`.
4. Run the project.
5. Put legal `.png` and `.wav` assets in `assets_local/` or edit the JSON
   manifests in `data/`.

## Input map

The project includes the expected input action names in `project.godot`:

```txt
fire_truth      -> left mouse / enter
fire_silencer   -> right mouse
focus           -> shift
lane_left       -> A / left arrow
lane_right      -> D / right arrow
rewind          -> R
advance_dialog  -> space / enter
```

## Data files

- `data/characters.json`
- `data/audio_manifest.json`
- `data/debate_math_trial_001.json`
- `data/logic_dive_problems_001.json`
- `data/game_config.json`

The goal is to build trials by editing data instead of hardcoding every scene
interaction.

## Verification

Run the lightweight project sanity check:

```bash
python scripts/verify_project.py
```

This checks required project files, validates JSON under `data/`, and reports
the number of C# source files.

## Status

This is a starter engine/prototype, not a complete game. The included content is
for educational/math-trial structure and engine testing. Bring your own legal
art, audio, UI, and story content before distributing a game built on it.

## License

MIT for the source code. See [LICENSE](LICENSE). Third-party names, brands, and
media are not included and are not licensed by this repository.
