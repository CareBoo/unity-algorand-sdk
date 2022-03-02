#!/usr/bin/env bash

set -euo pipefail


algosdk_target="Unity.AlgoSdk/Assets/unity-algorand-sdk"
for entry in Runtime Editor Tests README.md CHANGELOG.md LICENSE.md "Third Party Notices.md" package.json
do
cp -fRv "$entry" "$algosdk_target"
cp -fv "$entry.meta" "$algosdk_target"
done
for entry in "Documentation~"
do
cp -fRv "$entry" "$algosdk_target"
done

unitask_src="Unity.AlgoSdk/Packages/com.cysharp.unitask"
unitask_target="Unity.AlgoSdk/Assets/UniTask"
mkdir -p "$unitask_target"
for entry in Runtime Editor package.json
do
    cp -fRv "$unitask_src/$entry" "$unitask_target"
    cp -fv "$unitask_src/$entry.meta" "$unitask_target"
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
