#!/usr/bin/env bash
set -euo pipefail

# Build and run the project directly, with default argument if none provided
PROJECT_DIR="${PWD}/leetcode_3234"
DEFAULT_ARG="Alice"

if [ "$#" -gt 0 ]; then
  dotnet run --project "$PROJECT_DIR/leetcode_3234.csproj" -- "$@"
else
  dotnet run --project "$PROJECT_DIR/leetcode_3234.csproj" -- "$DEFAULT_ARG"
fi
