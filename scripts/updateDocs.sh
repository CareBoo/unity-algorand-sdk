#!/usr/bin/env bash

set -euo pipefail

docfx .docfx/docfx.json -t statictoc
cp -r _site/ Algorand.Unity.AssetStore/Assets/Algorand.Unity/Documentation/
