#!/usr/bin/env bash

set -euo pipefail

cd Unity.AlgoSdk

algosdk_src="Packages/com.careboo.unity-algorand-sdk"
algosdk_target="Assets/AlgoSDK"

unitask_src="Packages/com.cysharp.unitask"
unitask_target="Assets/UniTask"


for item in $algosdk_src/*
do
    cp -fRvL "$item" "$algosdk_target/$item"
done

for item in $unitask_src/*
do
    cp -fRvL "$item" "$unitask_target/$item"
done


touch packageMetaFiles

echo "$algosdk_target.meta" >> packageMetaFiles
find "$algosdk_target" -type f -name "*.meta" >> packageMetaFiles

echo "$unitask_target.meta" >> packageMetaFiles
find "$unitask_target" -type f -name "*.meta" >> packageMetaFiles

cd -
