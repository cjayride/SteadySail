# Steady Sail

Raising or lowering a sail no longer rolls the boat or launches the hull. For a short moment after the sail moves, roll is damped and upward speed is capped.

Install on the client and the dedicated server. The boat's owner is the one running the physics.

Config: `BepInEx/config/cjayride.SteadySail.cfg`

- **Settle seconds** (default 0.4): how long the roll stays damped after the sail reaches its new height.
- **Max upward speed** (default 1.5): upward speed cap, in meters per second, while that damping is active.
