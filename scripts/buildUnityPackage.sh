#!/usr/bin/env bash

set -euo pipefail

echo "Building unity package using unity packager"
echo "dotnet installed at \"$(which dotnet)\""

exporter="UnityPackager"
exporter_dir="$HOME/TwoTenPvP/UnityPackager"
if [ ! -d "$exporter_dir" ]
then
    echo "Could not find package exporter_dir at $exporter_dir" 1>&2
    exit 1
else
    echo "Found package exporter_dir at $exporter_dir"
fi

algosdk_dir="unity-algorand-sdk"
echo "Making Unity Algorand SDK dir at $algosdk_dir"
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
echo "Made Unity Algorand SDK dir"


unitask_url="https://github.com/Cysharp/UniTask/releases/download/2.2.5/UniTask.2.2.5.unitypackage"
wget -O UniTask.unitypackage "$unitask_url"
unitask_dir="UniTask"
echo "Making UniTask directory at $unitask_dir"
mkdir -p "$unitask_dir"
echo "Made unitask dir"

mkdir -p dist

workdir="$(pwd)"
cd "$exporter_dir"
dotnet run --project "$exporter" unpack "$workdir/UniTask.unitypackage" "$workdir/$unitask_dir"
dotnet run --project "$exporter" pack "$workdir/dist/unity-algorand-sdk.unitypackage" "$workdir/$algosdk_dir" "$workdir/$unitask_dir"
cd -
