# R Swipe League V2

### Arcade car-ball meets hoops

Drive a car, hit the ball, chain **jumps** and **boosts**, and score through the goal. **Aerial** touches and **dunks** from the paint zone score higher than grounded goals — miss or get blocked and you earn nothing. Built for a **fast reset** so every possession matters.

---

## About this project

**R Swipe League V2** is a **first-playable** built to answer one question: *does driving + jumping + dunking feel fun in the first two minutes?* The prototype keeps physics **readable** (capped hit assist, tuned damping) so skill reads louder than rng.

A **React + Tailwind** site ships alongside the game so you can pitch the same story on **GitHub** or **Devpost**: demo in Unity, narrative and scoring on the web.

---

## Core loop

**Drive** → **strike the ball** → **move it toward the goal** → **jump / boost / dunk** → **score** → **instant reset**

---

## What you get today

| Deliverable | Notes |
|-------------|--------|
| **Unity prototype** | One arena, one car, one ball, one goal, dunk trigger volume, HUD, OOB reset |
| **Scoring tiers** | **2** grounded goal · **3** aerial · **4** aerial + dunk zone |
| **Marketing site** | Feature grid, scoring cards, roadmap copy — no WebGL embed yet |

---

## Vision & novelty

**Roadmap (design — not all in this build):**

- **Style chain** — bonus for sequences like wall ride → aerial → dunk  
- **Dynamic goals** — hoop size / position shifts in overtime  
- **Arena gadgets** — bounce pads, gravity lanes, rebound walls  
- **Momentum meter** — short hype bonus after big plays  
- **1v1 / 2v2** — Photon Fusion or Netcode when feel is locked  
- **Training mode** — drills for boost lines and dunk timing  

**Why it’s different:** car-soccer **clarity** plus basketball-style **finish timing** and **tiered risk** (ground vs air vs dunk) — arcade speed without muddy ball physics.

---

## Table of contents

1. [Technical — stack](#technical--stack)  
2. [Technical — repository layout](#technical--repository-layout)  
3. [Technical — setup & run](#technical--setup--run)  
4. [Technical — controls & scoring](#technical--controls--scoring)  
5. [Technical — physics tuning](#technical--physics-tuning)  
6. [Links](#links)  

---

## Technical — stack

| Layer | Details |
|--------|---------|
| **Game engine** | Unity **6** (6000 LTS), **Input System**, **UGUI**, Unity 3D physics |
| **Web** | **Vite**, **React**, **TypeScript**, **Tailwind CSS v4** |
| **Planned** | Multiplayer: Photon Fusion / Netcode for GameObjects · Backend: PlayFab / Firebase |

---

## Technical — repository layout

```text
├── README.md
├── unity/                 Open in Unity Hub
│   ├── Assets/
│   │   ├── Scripts/       Gameplay, scoring, camera
│   │   └── Editor/        Prototype scene generator
│   └── ProjectSettings/
└── web/
    └── src/               Homepage (Vite + React)
```

---

## Technical — setup & run

### Web

```bash
cd web
npm install
npm run dev
```

Default URL: **http://localhost:5173**

```bash
cd web
npm run build
```

Artifacts: **`web/dist/`**

### Unity

1. Install **Unity Hub** and a **Unity 6** editor (≥ **6000.0.38f1** or Hub-recommended LTS).  
2. **Add** the **`unity`** folder.  
3. Wait for **Input System** + **UGUI** import.  
4. Menu: **R Swipe League V2 → Generate Prototype Scene**  
5. Open **`Assets/Scenes/Prototype.unity`** → **Play**.

---

## Technical — controls & scoring

### Inputs

| Input | Action |
|--------|--------|
| **W A S D** | Drive / steer |
| **Space** | Jump / double-jump |
| **Shift** | Boost (cooldown) |

### Scoring

| Type | Points | Rule (prototype) |
|------|--------|-------------------|
| Goal | **2** | Last touch while grounded |
| Aerial | **3** | Last touch while airborne |
| Dunk | **4** | Airborne touch from **dunk zone** |

---

## Technical — physics tuning

On the **Car** in the generated scene: **move acceleration**, **max speed**, **jump impulse**, **hit assist** / **max hit assist**. On the **Ball**: **mass**, **linear damping**, **BallPrototype** material under `Assets/Generated/PhysicsMaterials/`. Tune one variable at a time.

Package mismatch in Hub → use **Upgrade** or align package versions to your editor.

---

## Links

| Resource | URL |
|----------|-----|
| Repository | [github.com/AymanM7/car-ball-dunk](https://github.com/AymanM7/car-ball-dunk) |
| Maintainer | [github.com/AymanM7](https://github.com/AymanM7) |

**Devpost:** add a short demo video + screenshots of Play Mode and the homepage; you can paste the **top half** of this README as your story and link the repo for build steps.

