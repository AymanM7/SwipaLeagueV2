# R SWIPAA V2

**Arcade 3D car-ball game:** drive, strike the ball, jump and boost, score through the hoop — with aerial and dunk tiers. Includes a **Unity 6** first-playable and a **React + Tailwind** marketing site.


---

## Table of contents

1. [Overview](#overview)
2. [What's included](#whats-included)
3. [Tech stack](#tech-stack)
4. [Repository structure](#repository-structure)
5. [Getting started](#getting-started)
6. [Gameplay & controls](#gameplay--controls)
7. [Physics tuning](#physics-tuning)
8. [Roadmap & novelty](#roadmap--novelty)

---

## Overview

**R SWIPAA V2** is a fast, readable physics prototype: one arena, one car, one ball, and one goal. The design goal is *easy to learn, hard to master* — tight resets, clear scoring, and ball contact that feels **controlled**, not random.

The companion **web** experience explains the loop, scoring (2 / 3 / 4 points), and planned meta systems for judges or collaborators — the same idea as a **Devpost** submission: a playable demo plus a clear, skimmable story.

---

## What's included

| Piece | Description |
|--------|-------------|
| **Unity prototype** | Generated arena, arcade car controller, ball + goal triggers, dunk zone, HUD, out-of-bounds reset |
| **Web homepage** | Vite + React + TypeScript + Tailwind v4 landing page (features, scoring, roadmap copy) |
| **Editor workflow** | Menu command to (re)build the prototype scene and register it in Build Settings |

---

## Tech stack

| Layer | Technologies |
|--------|----------------|
| **Game** | Unity **6** (6000 LTS), **Input System**, **UGUI**, 3D physics |
| **Web** | **Vite**, **React**, **TypeScript**, **Tailwind CSS v4** |
| **Future (planned)** | Multiplayer: Photon Fusion or Netcode for GameObjects · Backend: PlayFab or Firebase |

---

## Repository structure

```text
├── README.md                 ← You are here
├── unity/                    ← Open this folder in Unity Hub
│   ├── Assets/
│   │   ├── Scripts/          Gameplay + scoring
│   │   └── Editor/           Scene generator (menu)
│   └── ProjectSettings/
└── web/                      ← Marketing / pitch site
    └── src/
```

---

## Getting started

### Web (homepage)

From the repo root:

```bash
cd web
npm install
npm run dev
```

Open the URL Vite prints (typically **http://localhost:5173**).

**Production build**

```bash
cd web
npm run build
```

Output: `web/dist/`.

### Unity (game prototype)

1. Install **[Unity Hub](https://unity.com/download)** and a **Unity 6** editor (6000 LTS; project targets **6000.0.38f1** or compatible).
2. **Add** → select the **`unity`** folder in this repo.
3. Open the project; wait for **Input System** and **UGUI** to import.
4. Menu: **R SWIPAA V2 → Generate Prototype Scene**  
   *Re-run after major script changes if you want a fresh scene layout.*
5. Open **`Assets/Scenes/Prototype.unity`**, press **Play**.

If Hub suggests a project upgrade, accept it unless your team is pinned to a specific patch.

---

## Gameplay & controls

| Input | Action |
|--------|--------|
| **W A S D** | Drive / steer |
| **Space** | Jump · double-jump |
| **Shift** | Boost (cooldown) |

### Scoring

| Type | Points | Condition (prototype) |
|------|--------|------------------------|
| Goal | **2** | Last touch while grounded (not aerial) |
| Aerial | **3** | Last touch while airborne |
| Dunk | **4** | Aerial touch **and** contact from **dunk zone** near the hoop |

Misses and blocks → **0**. Ball and car reset after a goal or out-of-bounds.

---

## Physics tuning

If strikes feel mushy or unfair, tune on the **Car** instance in the generated scene:

- **Move acceleration** / **max forward speed**
- **Jump impulse**
- **Hit assist impulse** / **max hit assist** (keeps hits readable)
- **Ball:** `Rigidbody` mass, **linear damping**, **BallPrototype** physic material under `Assets/Generated/PhysicsMaterials/`

Adjust one knob at a time and playtest in short sessions.

---

## Roadmap & novelty

**In design (not all implemented in code yet):**

- **Style chain** — combo rewards (e.g. wall ride → aerial → dunk)
- **Dynamic goals** — hoop changes in overtime
- **Arena gadgets** — bounce pads, gravity lanes, rebound walls
- **Momentum meter** — short bonus after big plays
- **1v1 / 2v2** — networking after feel is locked
- **Training mode** — drills for boost, reads, dunk timing

**Differentiator:** car-ball **clarity** (tuned contact, caps) combined with **hoop finish** timing and **tiered** scoring — arcade-fast without opaque physics.

---

## Links

| Resource | URL |
|----------|-----|
| Repository | [github.com/AymanM7/car-ball-dunk](https://github.com/AymanM7/car-ball-dunk) |
| Maintainer | [github.com/AymanM7](https://github.com/AymanM7) |

---

### Hackathon / Devpost tip

Add a **~60s demo video** and **2–3 screenshots** (Unity Play Mode + homepage) in your gallery. You can copy sections of this README directly into the Devpost **description** — tables and headings paste cleanly once you switch the editor to markdown (if available).
