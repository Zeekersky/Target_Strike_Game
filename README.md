
# Target Strike

**Target Strike** is a Unity-based shooting game that challenges a trained ML agent and a human player to compete in precision shooting by targeting randomly spawned objects in separate environments. The ML agent learns to maximize its score autonomously through reinforcement learning, while the human player tests their skills using manual controls. The game tracks and compares both players' scores to evaluate the performance difference between AI and human gameplay.

## Game Overview

In *Target Strike*, both the ML agent and the human player aim to score points by shooting at targets and avoiding obstacles. Rewards are awarded for successful shots, while penalties are applied for missed shots and collisions.

### Game Components

1. **ML Agent**: An AI-driven player that learns to navigate, aim, and shoot targets autonomously using reinforcement learning.
2. **Human Player**: A manually controlled player using standard movement and shooting controls in Unity.

## Features

- **Reinforcement Learning**: The ML agent learns effective strategies to target and shoot with minimal mistakes.
- **Reward System**: Dynamic in-game rewards and penalties are given based on shooting accuracy, target hits, and collision events.
- **Score Comparison**: Track total rewards for both the ML agent and the human player to evaluate the performance and efficiency of each.

## Reward Structure

1. **Target Hit**: +1 reward for successfully hitting a target with the laser.
2. **Missed Shot**: -0.2 penalty for each missed shot.
3. **Wall Collision**: -0.1 penalty if the player collides with a wall.
4. **Target Collision**: -0.05 penalty if the player collides directly with the target instead of shooting it.

## Project Structure

- **Scripts**
  - **AgentController.cs**: Handles movement, shooting mechanics, and reward tracking for the ML agent.
  - **PlayerController.cs**: Manages movement and shooting controls for the human player.
  - **GunController.cs**: Shared shooting functionality for both players, including laser visualization and hit detection.
  - **EnvBehavior.cs**: Manages the ML agent’s training ground, handling target spawning and resetting.
  - **PlayerEnvBehavior.cs**: Manages the human player’s training ground with target spawning and resetting.

- **Training Grounds**
  - **AgentTrainingGround**: Dedicated environment where the ML agent operates.
  - **PlayerTrainingGround**: Separate environment designed for human player gameplay.

## Getting Started

### Prerequisites

- **Unity 2020.3 or higher**
- **ML-Agents** Follow This [Github Repo of MLAgents](https://github.com/Unity-Technologies/ml-agents).

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/Target-Strike.git
   cd Target-Strike
   ```

2. **Open in Unity**:
   - Open the project in Unity and ensure all necessary packages are imported, especially `ML-Agents`.

### Scene Setup

1. **Two Scenes `Menu` and `TargetStrike`** are there in the `Scene` folder.
2. Ensure that each object has the appropriate components and scripts assigned in the Inspector.

## Gameplay Instructions

### ML Agent

1. Run the Unity scene, and the ML agent will play autonomously, attempting to maximize its total reward.
2. **Rewards**: The top side of game screen show the agent’s rewards and player's rewards to show learning progression and success.

### Human Player

1. Use the **PlayerController** to control the human player in `PlayerTrainingGround`.
2. **Controls**:
   - **Move**: Use `WASD` or arrow keys for navigation.
   - **Shoot**: Press `Space` to shoot a laser toward the target.
3. **Score Tracking**: The console logs the human player's total rewards to allow performance comparison with the ML agent.

## Key Scripts

- **AgentController.cs**: Implements the ML agent’s autonomous movement, shooting, and reward mechanics.
- **PlayerController.cs**: Provides manual controls to the human player with behavior similar to the ML agent.
- **GunController.cs**: Manages shooting mechanics, including raycasting for hit detection.
- **EnvBehavior.cs**: Controls ML agent environment setup, target spawning, and reward logging.
- **PlayerEnvBehavior.cs**: Controls the human player environment setup and target spawning.

## Reward Tracking

- **Score Logging**: Both players’ scores are logged in the console, showing total rewards after each episode for easy comparison. Also both are showing in corner of Game Screen.

## Debugging Tips

- **Episode Tracking**: The console logs the total rewards per episode, useful for observing performance trends.
- **Reward Tuning**: Adjust reward values in `AgentController.cs` and `PlayerController.cs` to influence agent behavior and encourage better accuracy.

## Contribution

Contributions are welcome! To contribute, fork the repository, make your changes, and submit a pull request.
