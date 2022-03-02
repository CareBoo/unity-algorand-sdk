#!/usr/bin/env bash

set -euo pipefail


echo "Building unity package using unity packager"


algosdk_dir="unity-algorand-sdk"
mkdir -p "$algosdk_dir"
for entry in Runtime Editor Tests README.md CHANGELOG.md LICENSE.md "Third Party Notices.md" package.json
do
cp -fRv "$entry" "$algosdk_dir"
cp -fv "$entry.meta" "$algosdk_dir"
done
for entry in "Documentation~"
do
cp -fRv "$entry" "$algosdk_dir"
done

unitask_root="Unity.AlgoSdk/Packages/com.cysharp.unitask"
unitask_dir="UniTask"
mkdir -p "$unitask_dir"
for entry in Runtime Editor package.json
do
    cp -fRv "$unitask_root/$entry" "$unitask_dir"
    cp -fv "$unitask_root/$entry.meta" "$unitask_dir"
done


tmpdir="$HOME"
packager="UnityPackager"
packager_dir="$tmpdir/$packager"
mkdir -p "$packager_dir"
wget -c "https://github.com/TwoTenPvP/UnityPackager/archive/refs/tags/v1.2.5.tar.gz" -O - | tar -xz -C "$packager_dir"
packager_dir="$packager_dir/$packager-1.2.5"


mkdir -p dist
workdir="$(pwd)"
cd "$packager_dir"
ls "$workdir"
ls "$workdir/$algosdk_dir"
ls "$workdir/$unitask_dir"
echo "Packing $workdir/$algosdk_dir and $workdir/$unitask_dir"
dotnet run -f netcoreapp2.1 --project "$packager" pack "$workdir/dist/unity-algorand-sdk.unitypackage" "$workdir/$algosdk_dir" "$workdir/$unitask_dir"
cd -
