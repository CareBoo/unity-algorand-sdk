#!/usr/bin/env zsh

set -euo pipefail

algosdk_src="Unity.AlgoSdk/Packages/com.careboo.unity-algorand-sdk"
algosdk_target="Unity.AlgoSdk/Assets/AlgoSdk"
unitask_src="Unity.AlgoSdk/Packages/com.cysharp.unitask"
unitask_target="Unity.AlgoSdk/Assets/UniTask"

for item in $algosdk_src/*
do
    cp -fRv "$item" $algosdk_target
done

for item in $unitask_src/*
do
    cp -fRv "$item" $unitask_target
done

mkdir -p dist
touch dist/packageMetaFiles

echo "$algosdk_target.meta" >> dist/packageMetaFiles
for metaFile in $algosdk_target/**/*.meta
do
    echo "$metaFile" >> dist/packageMetaFiles
done

echo "$unitask_target.meta" >> dist/packageMetaFiles
for metaFile in $unitask_target/**/*.meta
do
    echo "$metaFile" >> dist/packageMetaFiles
done
