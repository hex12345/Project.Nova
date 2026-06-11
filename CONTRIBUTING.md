# Contributing

Contributions are welcome. Keep changes focused and make sure the project still
passes the lightweight sanity check:

```bash
python scripts/verify_project.py
```

## Guidelines

- Use original, public-domain, or properly licensed assets only.
- Keep copyrighted third-party game assets, music, sprites, logos, and voices
  out of the repository.
- Prefer data-driven trial content in `data/` over hardcoded scene behavior.
- Update docs when changing scene wiring, JSON schemas, or input actions.
- Include a short test note in pull requests.

## Pull Request Checklist

- Explain what changed and why.
- Run `python scripts/verify_project.py`.
- Mention whether the change requires Godot editor setup or new assets.
- Confirm any added media files are legally redistributable.
