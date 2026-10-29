# Steady Sail

Keep your boat stable when raising or lowering the sail in Valheim.

Steady Sail prevents sudden boat rolling and unexpected upward launches caused by sail adjustments. After the sail reaches its new position, the mod briefly dampens the boat's roll and limits its upward speed to help the hull settle smoothly.

## Features

- **Stable Sail Adjustments** — Reduces sudden rolling when raising or lowering the sail.
- **Launch Prevention** — Caps upward velocity to help prevent the boat from being launched into the air.
- **Configurable Settling** — Adjust how long stabilization remains active after changing the sail.
- **Adjustable Upward Speed** — Set the maximum upward velocity while stabilization is active.

## Configuration

Config file:

`BepInEx/config/cjayride.SteadySail.cfg`

| Setting | Default | Description |
|---|---:|---|
| `Settle seconds` | `0.4` | Duration in seconds that roll damping remains active after the sail reaches its new height. |
| `Max upward speed` | `1.5` | Maximum upward speed in meters per second while roll damping is active. |

## Installation

Install **Steady Sail** on both the **client and dedicated server**.

The boat's owner runs the physics simulation, so the mod needs to be installed where the boat's owner is playing.
