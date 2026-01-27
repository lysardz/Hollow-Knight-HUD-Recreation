# Hollow-Knight-HUD-Recreation
</picture>
<img src="https://github.com/lysardz/Hollow-Knight-HUD-Recreation/blob/main/Final.gif" alt="Alt text">
</picture>

## Recreated the Hollow Knight UI using:

- **Shader Graph** for stylized effects.

- **VFX Graph** for particle animations

- **DOTween** for material and rect transform based animations.

- Unity **uGUI** canvas setup

This was my first project messing with shaders and animating them. I set up interactions in C# and used dotween to change material values with different tweening settings, that are accessable in the inspector.


This project is published on itch and has a downloadable build that is interactable here: https://lysard.itch.io/hk-hud-fan-recreation


I will note there are a few things I have learnt since making this project.

- I was killing tweens in this project and have since moved to reusing them and using restart on them.
  
- I had used the update for the fluid effect animations, but have since learned of the dotween tweeners that allow material changes without checking the update.

- Since doing this project I have updated my workflow to include custom nodes with hlsl logic, and currently leaning into learning to use the code more exclusively.
