#!/usr/bin/env python3
"""Build a manifest skeleton from local PNG/WAV assets."""

from __future__ import annotations
import argparse
import json
from pathlib import Path


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--asset-root", default="assets_local")
    parser.add_argument("--out", default="data/generated_assets.json")
    args = parser.parse_args()

    root = Path(args.asset_root)
    characters_root = root / "characters"
    sfx_root = root / "sfx"

    characters: dict[str, dict] = {}
    if characters_root.exists():
        for char_dir in sorted(p for p in characters_root.iterdir() if p.is_dir()):
            sprites = {}
            for png in sorted(char_dir.glob("*.png")):
                pose = png.stem
                sprites[pose] = f"res://{png.as_posix()}"
            characters[char_dir.name] = {
                "displayName": char_dir.name.replace("_", " ").title(),
                "sprites": sprites,
            }

    sfx = {}
    if sfx_root.exists():
        for wav in sorted(sfx_root.glob("*.wav")):
            sfx[wav.stem] = f"res://{wav.as_posix()}"

    out = {
        "characters": characters,
        "sfx": sfx,
    }

    out_path = Path(args.out)
    out_path.parent.mkdir(parents=True, exist_ok=True)
    out_path.write_text(json.dumps(out, indent=2), encoding="utf-8")
    print(f"wrote {out_path}")


if __name__ == "__main__":
    main()
