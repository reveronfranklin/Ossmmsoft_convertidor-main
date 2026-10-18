#!/usr/bin/env bash
set -euo pipefail
: "${GITHUB_SHA:?}" "${RUNNER_TEMP:?}"
artifact="$RUNNER_TEMP/qa-publish"
dotnet publish Convertidor/Convertidor.csproj -c Release -r linux-x64 --self-contained true -o "$artifact"
# Las conexiones y secretos solo existen en shared/config del Ubuntu.
rm -f "$artifact"/appsettings*.json
printf '%s\n' "$GITHUB_SHA" > "$artifact/DEPLOY_COMMIT"
printf 'legacy\n' > "$artifact/DEPLOY_COMPONENT"
tar -C "$artifact" -czf "$RUNNER_TEMP/qa-artifact.tar.gz" .
