# Unity Algorand SDK

[![Test Status](https://github.com/CareBoo/unity-algorand-sdk/actions/workflows/test.yaml/badge.svg)](https://github.com/CareBoo/unity-algorand-sdk/actions/workflows/test.yaml)
[![openupm](https://img.shields.io/npm/v/com.careboo.unity-algorand-sdk?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.careboo.unity-algorand-sdk/)
[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)](http://commitizen.github.io/cz-cli/)

> [!Caution]
> This package has not been audited and isn't suitable for production use.

Integrate Algorand blockchain services into your game.

Supported Build Targets:

- [x] Windows
- [x] Mac OS
- [x] Linux
- [x] Android
- [x] iOS
- [x] WebGL

## Installation

This SDK is provided as a UPM package in the following locations:

- [Open UPM](https://openupm.com/packages/com.careboo.unity-algorand-sdk)
- [NPM Registry](https://www.npmjs.com/package/com.careboo.unity-algorand-sdk)
- [GitHub Package Registry](https://github.com/CareBoo/unity-algorand-sdk/packages/894742)

### Open UPM

The easiest way to install is to use Open UPM as it manages your scopes automatically.
You can [install Open UPM here](https://openupm.com/docs/getting-started.html).
Then use the Open UPM CLI at the root of your Unity project to install.

```sh
> cd <your unity project>
> openupm add com.careboo.unity-algorand-sdk
```

### Manually Adding UPM Scopes

If you don't want to use Open UPM, it's straightforward to manually add the UPM registry scopes
required for this package.

1. In the Unity Editor, click on Edit -> Project Settings -> Package Manager.
2. Under the Scoped Registries tab, click the `+` button to add a new Scoped Registry.
3. Set the following values for the Scoped Registry:

```yml
Name: Open UPM
URL: https://package.openupm.com
Scopes:
  - com.cysharp.unitask
  - com.careboo.unity-algorand-sdk
```

4. Click on Window -> Package Manager, and the `Algorand SDK` package should appear in the
   `Packages: My Registries` tab.
5. Install the latest version of the `Algorand SDK`.

For more details, see [Unity's official documentation on Scoped Registries](https://docs.unity3d.com/Manual/upm-scoped.html).

### Unity Asset Store

This SDK will soon be [available in the Unity Asset Store](https://u3d.as/2GBr).

## Getting Started

The [Manual](https://careboo.github.io/unity-algorand-sdk/main/) is the best place to start.
Check out the [Quickstart Guide](https://careboo.github.io/unity-algorand-sdk/main/manual/quickstart.html).

There's a Demo game you can try out in the [demo branch](https://github.com/CareBoo/unity-algorand-sdk/tree/demo).

> [!Note]
> The documentation is generated per release branch and version. The documentation for a specific version/branch
> can be found at `https://careboo.github.io/unity-algorand-sdk/{version}/`, e.g.
> https://careboo.github.io/unity-algorand-sdk/1.0.0-pre.3/
