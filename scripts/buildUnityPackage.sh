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


unitask_url="https://github.com/Cysharp/UniTask/releases/download/2.2.5/UniTask.2.2.5.unitypackage"
wget -O UniTask.unitypackage "$unitask_url"
unitask_dir="UniTask"
mkdir -p "$unitask_dir"


tmpdir="$HOME"
packager="UnityPackager"
packager_dir="$tmpdir/$packager"
wget -c "https://github.com/TwoTenPvP/UnityPackager/archive/refs/tags/v1.2.5.tar.gz" -O - | tar -xz -C "$packager_dir"


mkdir -p dist
workdir="$(pwd)"
cd "$packager_dir"
dotnet run --project "$packager" unpack "$workdir/UniTask.unitypackage" "$workdir/$unitask_dir"
dotnet run --project "$packager" pack "$workdir/dist/unity-algorand-sdk.unitypackage" "$workdir/$algosdk_dir" "$workdir/$unitask_dir"
cd -
