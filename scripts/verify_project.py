#!/usr/bin/env python3
"""Light sanity checker for the starter project."""

from __future__ import annotations
import json
from pathlib import Path

REQUIRED = [
    "project.godot",
    "TrialEngine.csproj",
    "scenes/Main.tscn",
    "data/characters.json",
    "data/audio_manifest.json",
    "data/debate_math_trial_001.json",
    "data/logic_dive_problems_001.json",
]


def load_json(path: Path) -> None:
    try:
        json.loads(path.read_text(encoding="utf-8"))
        print(f"json ok: {path}")
    except Exception as exc:
        print(f"json bad: {path}: {exc}")


def main() -> None:
    root = Path.cwd()
    for rel in REQUIRED:
        p = root / rel
        print(("ok" if p.exists() else "missing") + f": {rel}")

    for p in (root / "data").glob("*.json"):
        load_json(p)

    cs_files = list((root / "src").rglob("*.cs"))
    print(f"csharp files: {len(cs_files)}")


if __name__ == "__main__":
    main()
