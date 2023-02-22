#!/usr/bin/env bash

set -euo pipefail

find Algorand.Unity.AssetStore/Assets/Algorand.Unity/Documentation -type f \! -name '*.meta' -delete
docfx .docfx/docfx.json -t statictoc
cp -r _site/ Algorand.Unity.AssetStore/Assets/Algorand.Unity/Documentation/
