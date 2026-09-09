#!/usr/bin/env bash
# Lab 4.6 — canary flavor via slot traffic splitting (bash; set SUFFIX first)
# Run one command at a time.

# 1. Start the canary: send 10% of production traffic to the staging slot
az webapp traffic-routing set -g rg-quoteboard-$SUFFIX -n app-quoteboard-$SUFFIX --distribution staging=10

# Watch: the page's Slot/Instance labels (about 1 request in 10 answers from staging),
# and App Insights -> Live Metrics shows both slots taking traffic.

# 2. Check the current split (empty output = no split)
az webapp traffic-routing show -g rg-quoteboard-$SUFFIX -n app-quoteboard-$SUFFIX -o table

# 3. End the canary: all traffic back to production
az webapp traffic-routing clear -g rg-quoteboard-$SUFFIX -n app-quoteboard-$SUFFIX
