# Asset Pipeline

## Legal note

Do not ship ripped copyrighted sprites, voices, music, or UI from existing games. Put your own assets in:

```txt
assets_local/characters/<character_id>/<pose>.png
assets_local/sfx/<name>.wav
```

## Character manifest

`data/characters.json` maps character IDs to sprite paths.

```json
{
  "characters": {
    "makoto": {
      "displayName": "Makoto Naegi Placeholder",
      "sprites": {
        "neutral": "res://assets_local/characters/makoto/neutral.png"
      }
    }
  }
}
```

## Audio manifest

`data/audio_manifest.json` maps sound IDs to wav files.

```json
{
  "sfx": {
    "truth_fire": "res://assets_local/sfx/truth_fire.wav"
  }
}
```

## Helper script

Run:

```bash
python scripts/build_asset_manifest.py --asset-root assets_local --out data/generated_assets.json
```

It scans local PNG/WAV files and writes a manifest skeleton.
