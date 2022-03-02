#!/usr/bin/env bash

set -euo pipefail

echo "changing directory to Unity.AlgoSdk, pwd: $(pwd)"
cd Unity.AlgoSdk
echo "now in directory: $(pwd)"

algosdk_src="Packages/com.careboo.unity-algorand-sdk"
algosdk_target="Assets/AlgoSDK"

unitask_src="Packages/com.cysharp.unitask"
unitask_target="Assets/UniTask"


for item in $algosdk_src/*
do
    echo "now in directory: $(pwd)"
    cp -RvL "$item" "$algosdk_target/"
done

for item in $unitask_src/*
do
    echo "now in directory: $(pwd)"
    cp -RvL "$item" "$unitask_target/"
done


touch packageMetaFiles

echo "$algosdk_target.meta" >> packageMetaFiles
find "$algosdk_target" -type f -name "*.meta" >> packageMetaFiles

echo "$unitask_target.meta" >> packageMetaFiles
find "$unitask_target" -type f -name "*.meta" >> packageMetaFiles

cd -
