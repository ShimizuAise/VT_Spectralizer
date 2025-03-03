# About: 

VT_Spectralizer by ShimizuAise:

This App will create 9 Parameters in VTube Studio. 

I will do new releases as I finish important features or bugfixes. If you want a feature that is complete but not in a release, you can build the app from the source code.

# Parameters

VTSpec_toggle: This is a toggle controllable via this App's UI with 5 selectable values. Range: 0-1

VTSpec_subBass: Volume of SubBass Frequencies, 20 Hz to 60 Hz, Range: 0-100

VTSpec_bass: Volume of Bass Frequencies, 60 Hz to 250 Hz, Range: 0-100

VTSpec_lowMid: Volume of Low-Mid Frequencies, 250 Hz to 500 Hz, Range: 0-100

VTSpec_midRange: Volume of Mid-Range Frequencies, 500 Hz to 2 kHz, Range: 0-100

VTSpec_upperMid: Volume of Upper-Mid Range Frequencies, 2 kHz to 4 kHz, Range: 0-100

VTSpec_presence: Volume of High Range Frequencies: 4 kHz to 6 kHz, Range: 0-100

VTSpec_brilliance: Volume of Highest Range Frequencies:	6 kHz to 20 kHz, Range: 0-100

VTSpec_volume: Volume of the Highest Volume Frequency. Range: 0-100

# Setup

For VTubers:

1. Download the app zip file from the Releases page, then extract it to anywhere on your pc. Its just a zip file, with no installation needed. (To Uninstall, just delete it)

2. Open the app along with your VTubeStudio.

3. In VT Spectralizer start audio capture, then Connect to VTubeStudio.

For Riggers using Live2D's VTS connection for previews:

1. You will need to connect VT_Spectralizer to VTubeStudio at least once before the parameters will show up during model setup.

2. If you want to use Live2D's live preview, the Recommended order for setup is: Export model with the params you want to use. Set up the model's params in VTubeStudio, then continue rigging.

# Shilling

This app is completely free and open source.

If you want to support me, follow me on Twitch at https://www.twitch.tv/shimizuaise or donate at https://streamlabs.com/shimizuaise/tip

My Socials are: https://x.com/ShimizuAise and https://bsky.app/profile/shimizuaise.bsky.social 

# Contribute

If you want to contribute, you can submit pull requests, or report bugs.

# Issues and Backlog

1: As of right now, the App does not support changing the default connection IP address or Port. Use LocalHost port 8001 (the default for VtubeStudio)

2: Done: ~~Adaptive Volume Ratios: Right now if a loud noise is processed, the volume ratio will permanently be stuck based on the singular loud noise. Next version will have adaptive volume ratios to remedy this issue.~~

3: ToDo: Option to split left/right tracks for stereo audio and double the number of parameters.

4: ToDo: Change the param range from 0-100 to 0-1.

5: ToDo: Better UI Design to move away from default windows design.

6: Done: ~~Allow selection of audio input device for users of Virtual Audio Cable and similar apps.~~
