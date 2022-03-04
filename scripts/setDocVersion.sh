#!/usr/bin/env bash

set -euo pipefail

echo "Setting documentation urls to the latest version"

jq --arg url "https://careboo.github.io/unity-algorand-sdk/" \
    '.documentationUrl = $url + .version | .changelogUrl = $url + .version + "/changelog" | .licensesUrl = $url + .version + "/license"' \
    package.json > package.json.tmp \
    && rm package.json \
    && mv package.json.tmp package.json
