#!/usr/bin/env bash

set -euo pipefail

cd Unity.AlgoSdk

algosdk_src="Packages/com.careboo.unity-algorand-sdk"
algosdk_target="Assets/AlgoSdk"

unitask_src="Packages/com.cysharp.unitask"
unitask_target="Assets/UniTask"


for item in $algosdk_src/*
do
    cp -RvL "$item" $algosdk_target
done

for item in $unitask_src/*
do
    cp -RvL "$item" $unitask_target
done


rm -rf $algosdk_src/*
rm -rf $unitask_src/*

cd -
