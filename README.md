# Car Ball Dunk — first playable + homepage

This repo contains a Unity 6 prototype (arcade car, ball, hoop, dunk zone, scoring) and a Vite + React + Tailwind marketing page.

## Web homepage

```bash
cd web
npm install
npm run dev
```

Then open the URL Vite prints (default `http://localhost:5173`).

## Unity prototype

1. Install **Unity 6** via Unity Hub (project expects editor **6000.0.38f1** or newer in the 6000 LTS line; Hub may upgrade the project slightly on first open).
2. **Add project** pointing at the `unity` folder in this directory.
3. Open the project and let the Editor finish importing packages (`Input System`, `UGUI`).
4. From the menu bar, choose **Car Ball Dunk → Generate Prototype Scene** (or run it again to refresh the scene after script changes).
5. Open `Assets/Scenes/Prototype.unity` if it is not already open, then press **Play**.
6. Controls (new Input System): **WASD** drive, **Space** jump / double-jump, **Shift** boost. Score populates the HUD; goals reset ball and car after a short delay.

### Physics tuning (readable hits)

If hits feel mushy or random, adjust on the **Car** prefab instance in the generated scene:

- **Move acceleration / max forward speed** — how quickly the car reaches top speed.
- **Jump impulse** — vertical pop; lower if double jumps feel too floaty.
- **Hit assist impulse / max hit assist** — caps the extra impulse applied on ball contact so strikes stay predictable.
- **Ball** `Rigidbody` mass, `linear damping`, and the **BallPrototype** physic material (`Generated/PhysicsMaterials`) for bounce and friction.

If the editor reports package version mismatches, use Hub’s **Upgrade** or match versions to your installed Unity 6 release.

---

## About this project

**Car Ball Dunk** is an arcade **3D “car meets hoops”** experiment: you drive a small physics car, strike a ball, and try to **finish through a goal**—with **jump chains**, **boost**, and a **dunk volume** near the hoop that rewards riskier aerial finishes. The core loop is intentionally tight: **drive → hit → set up → jump or boost → score → fast reset**, so each possession stays readable and snappy instead of turning into a long reset walk.

The **Unity** side is a **first playable**: one arena, one car, one ball, one goal, and a scoring model (**2** grounded goal, **3** aerial touch, **4** dunk when the last touch was aerial *and* from the dunk zone). Physics are tuned around **controlled contact**—capped hit assist and deliberate damping—so outcomes feel skill-based rather than lottery-like. The **web** app is a **React + Tailwind** landing page that presents the pitch, roadmap features, and how scoring works; it does not embed the game (you’d add a WebGL build later if you want that).

**What’s novel (design direction, not all built yet in code):** stacking **style** (e.g. wall ride → aerial → dunk for combo bonuses), **dynamic goals** in overtime, **arena gadgets** (bounce pads, gravity lanes, rebound walls), and a **momentum / hype meter** that briefly rewards big plays. The repo’s “novelty” today is the **hybrid fantasy**—Rocket League–style car-ball clarity with basketball-style **dunk timing** and point tiers—plus a path to multiplayer (**Photon Fusion** or **Netcode**) and backend (**PlayFab** / **Firebase**) once the single-player feel is nailed.
