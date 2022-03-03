#!/usr/bin/env bash

set -euo pipefail

cd Unity.AlgoSdk

algosdk_src="Packages/com.careboo.unity-algorand-sdk"
algosdk_target="Assets/AlgoSdk"

cp -RvL $algosdk_src/* $algosdk_target
ls -1 $algosdk_src | grep -v "package.json" | xargs -I {} rm -rf {}


unitask_src="Packages/com.cysharp.unitask"
unitask_target="Assets/UniTask"

cp -RvL $unitask_src/* $unitask_target
ls -1 $unitask_src | grep -v "package.json" | xargs -I {} rm -rf {}

cd -
