# Swift Runner — Endless Runner

## 🧩 Summary
Swift Runner is a fast, third-person endless runner set on a floating castle road. Dodge physics-driven traps, weave through collapsing props, and grab coins and boosters to stretch your streak and chase high scores. Procedural bridge segments keep each run fresh while a lightweight URP presentation and clean C# make it easy to build on. 

- Session length: 1–3 min arcade loops  
- Platforms: PC / WebGL today; mobile-ready structure  
- Focus: responsive feel, readable code, and modular prefabs

## ✨ Features
- Procedural track built from reusable chunk prefabs (endless flow).
- Collectibles & boosters: coins (+score) and time pickups to extend runs.
- Obstacle system: falling barrels, carts, rocks; tuned physics materials.
- Juice & readability: camera shake, particles, clean HUD (timer/score).
- Managers & SOs: GameManager, ScoreManager, ScriptableObject catalogs.
- URP + TMP + Input System out of the box.

## 🎮 Controls (default; editable)
- Move Up / Down / Left / Right — W / S / A / D or ↑ / ↓ / ← / →  
- Jump - Space Bar

## 🛠 Tech Highlights
- Unity 6.2 LTS, URP, TextMesh Pro, Input System (.inputactions).
- Data-driven setup with ScriptableObjects (segment catalogs, tuning).
- Clear separation: Managers/, Proc Gen/, Prefabs/Chunks, Prefabs/Falling Obstacles, Resources/.
- Physics tuned via PhysicMaterial assets for consistent collisions.
- Basic pooling hooks prepared; fixed timestep tuned for smooth feel on mid-range hardware. 

## ▶️ How to Run
1. Clone the repo.
2. Open in Unity (6.2 version preferred)
3. Open the main scene:  
   `Royal Run/Assets/Scenes/MainLevel.unity`
4. Press Play.

## 📸 Screenshots
<img width="820" height="460" alt="Royal Run1" src="https://github.com/user-attachments/assets/769a8625-6dbe-48a9-963b-fb6516ed1e2f" />
<img width="934" height="522" alt="Royal Run" src="https://github.com/user-attachments/assets/e5dc471c-bd21-4ece-a1de-ffef1ad8bf40" />

## 🧭 Roadmap 
- Difficulty curve & speed ramp
- Object pooling for spawners/hazards
- Touch controls & haptics (mobile)
- Power-up (apple)
