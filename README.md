<h1 align="center">
<img src="Documentation~/images/logo_256.png"/>

Unity Algorand SDK

</h1>
<p align="center">
  <a href="LICENSE.md">
    <img src="https://img.shields.io/github/license/CareBoo/unity-algorand-sdk"/>
  </a>
  <a href="https://github.com/CareBoo/unity-algorand-sdk/actions/workflows/test.yaml">
    <img src="https://img.shields.io/github/workflow/status/careboo/unity-algorand-sdk/Unity%20Tests/main?label=tests"/>
  </a>
  <a href="https://www.npmjs.com/package/com.careboo.unity-algorand-sdk">
    <img src="https://img.shields.io/npm/v/com.careboo.unity-algorand-sdk"/>
  </a>
  <a href="https://openupm.com/packages/com.careboo.unity-algorand-sdk/">
    <img src="https://img.shields.io/npm/v/com.careboo.unity-algorand-sdk?label=openupm&registry_uri=https://package.openupm.com"/>
  </a>
</p>

> [!Caution]
> This package has not been audited and isn't suitable for production use.

This package serves as an SDK for [Algorand](https://www.algorand.com/), a Pure Proof-of-Stake blockchain overseen by the Algorand Foundation.
Create and sign Algorand transactions, use Algorand's [REST APIs](https://developer.algorand.org/docs/rest-apis/restendpoints/),
and connect to any Algorand wallet supporting [WalletConnect](https://developer.algorand.org/docs/get-details/walletconnect/).

## Supported Targets

| Unity Version | Windows            | Mac OS             | Linux              | Android            | iOS                | WebGL              |
| ------------- | ------------------ | ------------------ | ------------------ | ------------------ | ------------------ | ------------------ |
| 2020.3        | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: |
| 2021.2        | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: |

## Installation

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

### Documentation

The [Quickstart Guide](Documentation~/quickstart.md) is the best place to start. You can view
the documentation website for a specific version at

```
https://careboo.github.io/unity-algorand-sdk/{version}/
```

### Demo

There's a Demo game you can try out in the [demo branch](https://github.com/CareBoo/unity-algorand-sdk/tree/demo).
