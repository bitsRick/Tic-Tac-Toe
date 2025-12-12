# Portfolio Project Tic-Tac-Toe  

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=for-the-badge&logo=unity)](https://unity3d.com)

Hierarchical N-ary Tree architecture with reactive programming.

## Contents
  -  About the Project
  -  Architectural scheme
  -  Implementation Features
  -  Quick Start
  -  Project Structure
  -  Credits
  -  License


## About the Project

N-Tic-Tac-Toe with N-ary Tree topology is a demonstration of a hierarchical architecture, where each component is organized as a tree node with an arbitrary number of child elements.
Key concepts:
- N-ary Tree as the basis of the architecture
- DI container for dependency management
Advantages of the N-Tree architecture:
- Scalability – easy to add new nodes
 
##  Architectural scheme

```
BootstrapScope (Root)
├── LoadingScope
│ └── LoadingFlow
├── MetaScope
│ └── MetaFlow
└── MatchScope
└── MatchFlow
```

## Implementation Features
- 🧩 DI integration
- ⚡ UniTask for asynchronous workflows
- 🔄 UniRx for Events

## Quick Start

### Requirements

```bash
Unity 6000.0.58f2+ 
UniRx 7.1.0+
VContainer 1.13.1+
UniTask 2.3.1+
```

 ### Installation
```bash
# Cloning a repository
git clone https://github.com/bitsRick/Tic-Tac-Toe.git
cd Tic-Tac-Toe
```

 ### Launch

1.Open Assets/_Project/Scenes/0.Bootstrap.unity
2.Click Play


## Project Structure
```text
Assets/
├── 📂 _Project/                   
│   ├── 📂 Art/
│   ├── 📂 Audio/
│   ├── 📂 Configs/ 		# Main Configs
│   ├── 📂 Develop/		# Script folder
│   │       ├── 📂 GoldenDragon.Game/ 	
│   │        │       ├── 📂 Editor/ 	# Scripts for the Editor
│   │        │       ├── 📂 Runtime/ 	# Scripts for Runtime
│   │        │        │       ├── 📂 _Bootstrap/
│   │        │        │       │      ├── …….
│   │        │        │       │      ├── BootstrapScope.cs  	
│   │        │        │       │      ├── BootstrapFlow.cs      # Entry point 
│   │        │        │       ├── 📂 _Loading/
│   │        │        │       │      ├── …….
│   │        │        │       │      ├── LoadingScope.cs
│   │        │        │       │      ├── LoadingFlow.cs      # Entry point 
│   │        │        │       ├── 📂 _Match/
│   │        │        │       │      ├── …….
│   │        │        │       │      ├── MatchScope.cs
│   │        │        │       │      ├── MatchFlow.cs      # Entry point 
│   │        │        │       ├── 📂 _Meta/
│   │        │        │       │      ├── …….
│   │        │        │       │      ├── MetaScope.cs
│   │        │        │       │      ├── MetaFlow.cs      # Entry point 
│   │        │        │       ├── ………….
│   │        │       ├── 📂 Tests/ 	# Test scripts
│   ├── 📂 Scenes/
│   │       ├── 📂 TestScene/ 	
│   │       ├── 0.Bootstrap.unity	# Stage for launch
│   │       ├── 1.Loading.unity	
│   │       ├── 2.Meta.unity		
│   │       ├── 3.Match.unity		
│   │       ├── 4.Empty.unity		
```

## 📄  Credits
This project is based on the following open-source components:
### [unity-empty-project-template]( https://github.com/vangogih/unity-empty-project-template)
- **Author**: Aleksei Kozorezov (vangogih)
- **License**: MIT License
- **Usage**: Project structure and basic organization template
- **Changes**:
  - UniRx integrated
  - Tic-tac-toe implemented
  - Utility Ai (soft)

### Libraries used
- **[VContainer](https://github.com/hadashiA/VContainer)** - Dependency Injection
- **[UniRx](https://github.com/neuecc/UniRx)** - Reactive programming
- **[UniTask](https://github.com/Cysharp/UniTask)** - Asynchronous Operations

## 📜 License
This project is licensed under the MIT license.

