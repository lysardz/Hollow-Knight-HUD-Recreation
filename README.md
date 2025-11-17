# Hollow-Knight-HUD-Recreation
This is a recreation of the Hollow Knight hud in unity using shader graph, vfx graph and dotween for interactions. 

This was my first project messing with shaders and animating them. I set up interactions in C# and used dotween to change material values with different tweening settings, that are accessable in the inspector.
This project is published on itch and has a downloadable build that is interactable here: https://lysard.itch.io/hk-hud-fan-recreation

I will note there are a few things I have learnt later after this project that is an improvement on some of the methods I used here.
- I was killing tweens in this project and have since moved to reusing them and using restart on them.
- I had used the update for the fluid effect animations, but have since learned of the dotween tweeners that allow material changes without checking the update.
- Since doing this project I have updated my workflow to include custom nodes with hlsl logic, and currently leaning into learning to use the code more. Especially to transfer future projects to other engines. I will make those projects public here in the future but you can check out work and progress updates on my artstation: https://alyssacoetzee7.artstation.com/albums/8408452
